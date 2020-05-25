using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class GameTask : MonoBehaviour, Pong.IGoalObservable
    {
        private enum Scene : int
        {
            Initialize, // 初期化
            ShotBall,   // ボール打ち出し
            Playing,    // プレイ中
            GoalSeStart,// ゴールSE開始
            GoalSeWait, // ゴールSE終了待ち
            Scoring,    // 加点処理
            Ended,      // 終了
            ChangeScene,// シーン切り替え
        };

        private static readonly string WinResultMessage = "勝ち";
        private static readonly string LoseResultMessage = "負け";
        private static readonly string InterfaceResultMessage = "タッチするとメニューに戻ります";

        // ballオブジェクト
        private GameObject ball;
        // ballスクリプト
        private Pong.Ball ballScript;
        private float ballRadius = 0.0f;
        private Rigidbody2D ballRigidbody = null;
        // プレイヤーのスコア
        private Pong.PlayerScore score = new Pong.PlayerScore();
        private Pong.ScoreSubject scoreSubject = new Pong.ScoreSubject();
        private Pong.IGameRulable gameRule = new Pong.GameRuleTenPoint();
        // シーン管理変数
        private Scene scene;
        // タッチ
        private Mhl.ISingleTouchActionable touchAction = new Mhl.SingleTouchActionEditor();
        // プレイヤー
        private IBoardControllable[] boardController = new IBoardControllable[Pong.PlayerConstant.Count];
        // 板
        [SerializeField] public GameObject boardLeft;
        [SerializeField] public GameObject boardRight;
        private float[] boardHeights = new float[Pong.PlayerConstant.Count];
        private Rigidbody2D boardLeftRigidbody = null;
        private Rigidbody2D boardRightRigidbody = null;
        // カメラ
        private Camera mainCamera;
        // サウンド
        public AudioClip pointSound;
        private AudioSource audioSource;
        // 結果
        [SerializeField] private UnityEngine.UI.Text resultTextLeft = null;
        [SerializeField] private UnityEngine.UI.Text resultTextRight = null;
        [SerializeField] private UnityEngine.UI.Text resultTextInterface = null;
        bool isResultTouchPushing = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GameTask()
        {
            scene = Scene.Initialize;
        }

        // Start is called before the first frame update
        void Start()
        {
            scene = Scene.Initialize;

            mainCamera = FindObjectOfType<Camera>();
            UnityEngine.Assertions.Assert.IsNotNull(mainCamera);

            boardController[(int)Pong.PlayerConstant.Position.Left] =
                new Pong.BoardTouchController(Pong.PlayerConstant.Position.Left, touchAction, mainCamera);

            boardController[(int)Pong.PlayerConstant.Position.Right] =
                new Pong.BoardCpu1Controller();


            InitializeSound();
            InitializeResult();

        }

        // Update is called once per frame
        void Update()
        {
            switch (scene)
            {
                case Scene.Initialize:
                    SceneInitialize();
                    break;
                case Scene.ShotBall:
                    SceneShotBall();
                    break;
                case Scene.Playing:
                    ScenePlaying();
                    break;
                case Scene.GoalSeStart:
                    SceneGoalSeStart();
                    break;
                case Scene.GoalSeWait:
                    SceneGoalSeWait();
                    break;
                case Scene.Scoring:
                    SceneScoring();
                    break;
                case Scene.Ended:
                    SceneEnded();
                    break;
                case Scene.ChangeScene:
                    SceneChangeScene();
                    break;
            }
            touchAction.Update();
            //touchAction.PrintDifference();
        }

        private void SceneInitialize()
        {
            scene = Scene.ShotBall;

            // boardの情報を取得
            SetBoardInfo(Pong.PlayerConstant.Position.Left);
            SetBoardInfo(Pong.PlayerConstant.Position.Right);
        }

        private void SceneShotBall()
        {
            CreateBall();
            AddGoalObserver(ballScript);
            // ボールの情報を設定
            SetBallInfo();

            UpdateBoard();

            scene = Scene.Playing;
        }

        private void ScenePlaying()
        {
            UpdateBoard();
        }

        private void SceneGoalSeStart()
        {
            UpdateBoard();
            PlayPointSound();
            scene = Scene.GoalSeWait;
        }

        private void SceneGoalSeWait()
        {
            UpdateBoard();
            if (IsPointSoundPlaying() == false)
            {
                scene = Scene.Scoring;
            }
        }

        private void SceneScoring()
        {
            UpdateBoard();
            scene = Scene.ShotBall;
        }

        private void SceneEnded()
        {
            if (touchAction.IsTouchEnded())
            {
                if (isResultTouchPushing)
                {
                    // タッチしたままEndedにきた時に1度だけタッチを無効にする
                    isResultTouchPushing = false;
                }
                else
                {
                    scene = Scene.ChangeScene;
                }
            }
        }

        private void SceneChangeScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        }

        /// <summary>
        /// ゴールイベント
        /// </summary>
        /// <param name="position">ゴールした方のプレイヤー</param>
        public void Goal(PlayerConstant.Position position)
        {
            // Ball削除(オブザーバー解除してからオブジェクトを削除)
            RequestRemoveObserver(ballScript);
            Destroy(ball.gameObject);
            ballScript = null;
            ball = null;

            if (position == PlayerConstant.Position.Left)
            {
                Debug.Log("GameTask(Goal) Left");
            }
            else
            {
                Debug.Log("GameTask(Goal) Right");
            }
            // スコアの加算
            score.Add(position);
            Debug.Log("score left  " + score.Get(PlayerConstant.Position.Left));
            Debug.Log("score right " + score.Get(PlayerConstant.Position.Right));
            // スコア更新通知
            scoreSubject.NotifyObservers(score);

            // ゲーム終了したか
            if (gameRule.CheckEnd(score))
            {
                // 文字列の設定
                if (gameRule.GetWinner(score) == PlayerConstant.Position.Left)
                {
                    SetResultLeftWin();
                }
                else
                {
                    SetResultRightWin();
                }

                // タッチしたままEndにいった
                if (touchAction.IsDragging() || touchAction.IsTouchStationary())
                {
                    isResultTouchPushing = true;
                }

                scene = Scene.Ended;
            }
            else
            {
                scene = Scene.GoalSeStart;
            }

        }

        /// <summary>
        /// スコア通知を追加
        /// </summary>
        /// <param name="o">スコア通知オブザーバー</param>
        public void AddScoreObserver(Pong.IScoreObserverable o)
        {
            scoreSubject.Add(o);
        }

        /// <summary>
        /// Ballの生成
        /// </summary>
        private void CreateBall()
        {
            Pong.BallFactory factory = new Pong.BallFactory();
            ball = Instantiate(factory.Create(), new Vector3(0, 0, 0), Quaternion.identity);
            UnityEngine.Assertions.Assert.IsNotNull(ball);
            ballScript = ball.GetComponent<Ball>();
            UnityEngine.Assertions.Assert.IsNotNull(ballScript);
        }

        /// <summary>
        /// ゴール通知の登録
        /// </summary>
        /// <param name="balScript">Ballスクリプト</param>
        private void AddGoalObserver(Pong.Ball balScript)
        {
            ballScript.AddGoalObserver(this);
        }

        /// <summary>
        /// ゴール通知の解除リクエスト
        /// </summary>
        /// <param name="ballScript">Ballスクリプト</param>
        private void RequestRemoveObserver(Pong.Ball ballScript)
        {
            ballScript.RequestRemoveObserver(this);
        }

        /// <summary>
        /// ボード情報の設定
        /// </summary>
        /// <param name="pos">設定するボードの位置</param>
        private void SetBoardInfo(Pong.PlayerConstant.Position pos)
        {
            // Rigidbodyの取得とBoxCllider2DからHeightの取得
            BoxCollider2D bc = null;
            if (pos == Pong.PlayerConstant.Position.Left)
            {
                bc = boardLeft.GetComponent<BoxCollider2D>();
                boardLeftRigidbody = boardLeft.GetComponent<Rigidbody2D>();
                UnityEngine.Assertions.Assert.IsNotNull(boardLeftRigidbody);
            }
            else
            {
                bc = boardRight.GetComponent<BoxCollider2D>();
                boardRightRigidbody = boardRight.GetComponent<Rigidbody2D>();
                UnityEngine.Assertions.Assert.IsNotNull(boardRightRigidbody);
            }
            UnityEngine.Assertions.Assert.IsNotNull(bc);
            boardHeights[(int)pos] = bc.size.y;
        }

        /// <summary>
        /// ボールの情報設定
        /// </summary>
        private void SetBallInfo()
        {
            // ballのRigidbodyとradiusを取得
            ballRigidbody = ball.GetComponent<Rigidbody2D>();
            ballRadius = ball.GetComponent<CircleCollider2D>().radius;
        }

        /// <summary>
        /// Vector2をVector3に変換
        /// </summary>
        /// <param name="vector2">変換元</param>
        /// <returns>変換されたVector3</returns>
        private Vector3 ToVector3(Vector2 vector2)
        {
            return new Vector3(vector2.x, vector2.y, 0);
        }

        /// <summary>
        /// Vector3をVector2に変換
        /// </summary>
        /// <param name="vector3">変換元</param>
        /// <returns>変換されたVector2</returns>
        private Vector2 ToVector2(Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.y);
        }

        /// <summary>
        /// ボード情報を作成
        /// </summary>
        /// <param name="position">対象のボードの位置</param>
        /// <returns>ボード情報</returns>
        private BoardInfo CreateBoardInfo(Pong.PlayerConstant.Position position)
        {
            UnityEngine.Assertions.Assert.IsNotNull(boardHeights);
            if (position == Pong.PlayerConstant.Position.Left)
            {
                UnityEngine.Assertions.Assert.IsNotNull(boardLeftRigidbody);
                return new BoardInfo(boardHeights[(int)position], ToVector3(boardLeftRigidbody.position));
            }
            else
            {
                UnityEngine.Assertions.Assert.IsNotNull(boardRightRigidbody);
                return new BoardInfo(boardHeights[(int)position], ToVector3(boardRightRigidbody.position));
            }
        }

        /// <summary>
        /// ボール情報の作成
        /// </summary>
        /// <returns>ボール情報</returns>
        private BallInfo CreateBallInfo()
        {
            if (ballRigidbody == null)
            {
                return null;
            }
            UnityEngine.Assertions.Assert.IsNotNull(ballRigidbody);
            return new BallInfo(ballRadius, ToVector3(ballRigidbody.position));
        }

        private void UpdateBoard()
        {
            BoardInfo boardInfoLeft = CreateBoardInfo(PlayerConstant.Position.Left);
            BoardInfo boardInfoRight = CreateBoardInfo(PlayerConstant.Position.Right);
            BallInfo ballInfo = CreateBallInfo();
            boardInfoLeft = boardController[(int)Pong.PlayerConstant.Position.Left].MoveBoard(
                boardInfoLeft, ballInfo);
            boardLeftRigidbody.position = ToVector2(boardInfoLeft.Position);

            boardInfoRight = boardController[(int)Pong.PlayerConstant.Position.Right].MoveBoard(
                boardInfoRight, ballInfo);
            boardRightRigidbody.position = ToVector2(boardInfoRight.Position);
        }

        private void UpdateBoardLeft()
        {
            BoardInfo boardInfoLeft = CreateBoardInfo(PlayerConstant.Position.Left);
            BallInfo ballInfo = CreateBallInfo();
            boardInfoLeft = boardController[(int)Pong.PlayerConstant.Position.Left].MoveBoard(
                boardInfoLeft, ballInfo);
            boardLeftRigidbody.position = ToVector2(boardInfoLeft.Position);
        }

        /// <summary>
        /// サウンドの初期化
        /// </summary>
        private void InitializeSound()
        {
            // 設定されているAudio系のデータをチェック
            UnityEngine.Assertions.Assert.IsNotNull(pointSound);
            audioSource = GetComponent<AudioSource>();
            UnityEngine.Assertions.Assert.IsNotNull(audioSource);
        }

        /// <summary>
        /// ポイントサウンドの再生
        /// </summary>
        private void PlayPointSound()
        {
            audioSource.PlayOneShot(pointSound);
        }

        /// <summary>
        /// ポイントサウンドが再生されているか
        /// </summary>
        /// <returns>trueなら再生されている</returns>
        private bool IsPointSoundPlaying()
        {
            return audioSource.isPlaying;
        }

        /// <summary>
        /// 結果の初期化
        /// </summary>
        private void InitializeResult()
        {
            UnityEngine.Assertions.Assert.IsNotNull(resultTextLeft);
            UnityEngine.Assertions.Assert.IsNotNull(resultTextRight);
            UnityEngine.Assertions.Assert.IsNotNull(resultTextInterface);            
            resultTextLeft.text = "";
            resultTextRight.text = "";
            resultTextInterface.text = "";
        }

        /// <summary>
        /// 左プレイヤーの勝ち設定
        /// </summary>
        private void SetResultLeftWin()
        {
            UnityEngine.Assertions.Assert.IsNotNull(resultTextLeft);
            UnityEngine.Assertions.Assert.IsNotNull(resultTextRight);
            resultTextLeft.text = WinResultMessage;
            resultTextRight.text = LoseResultMessage;
            resultTextInterface.text = InterfaceResultMessage;
        }

        /// <summary>
        /// 右プレイヤーの勝ち設定
        /// </summary>
        private void SetResultRightWin()
        {
            UnityEngine.Assertions.Assert.IsNotNull(resultTextLeft);
            UnityEngine.Assertions.Assert.IsNotNull(resultTextRight);
            resultTextLeft.text = LoseResultMessage;
            resultTextRight.text = WinResultMessage;
            resultTextInterface.text = InterfaceResultMessage;
        }
    }
}