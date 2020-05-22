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
            GoalSeStart,  // ゴールSE開始
            GoalSeWait, // ゴールSE終了待ち
            Scoring,    // 加点処理
            Ended       // 終了
        };

        // ballオブジェクト
        private GameObject ball;
        // ballスクリプト
        private Pong.Ball ballScript;
        private float ballRadius = 0.0f;
        private Rigidbody2D ballRigidbody = null;
        // プレイヤーのスコア
        private Pong.PlayerScore score = new Pong.PlayerScore();
        private Pong.ScoreSubject scoreSubject = new Pong.ScoreSubject();
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

            boardController[(int)Pong.PlayerConstant.Position.Left] =
                new Pong.BoardTouchController(Pong.PlayerConstant.Position.Left, touchAction);
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
            }
            touchAction.Update();
            touchAction.PrintDifference();
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

            scene = Scene.Playing;
        }

        private void ScenePlaying()
        {
            BoardInfo boardInfoLeft = CreateBoardInfo(PlayerConstant.Position.Left);
            BallInfo ballInfo = CreateBallInfo();
            boardInfoLeft = boardController[(int)Pong.PlayerConstant.Position.Left].MoveBoard(
                boardInfoLeft, ballInfo);

            /*
            BoardInfo boardInfoRight = CreateBoardInfo(PlayerConstant.Position.Right);
            boardInfoRight = boardController[(int)Pong.PlayerConstant.Position.Right].MoveBoard(
                boardInfoRight, ballInfo);
            */
        }

        private void SceneGoalSeStart()
        {
            scene = Scene.GoalSeWait;
        }

        private void SceneGoalSeWait()
        {
            scene = Scene.Scoring;
        }

        private void SceneScoring()
        {
            scene = Scene.ShotBall;
        }

        private void SceneEnded()
        {
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

            scene = Scene.GoalSeStart;
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
            UnityEngine.Assertions.Assert.IsNotNull(ballRigidbody);
            return new BallInfo(ballRadius, ToVector3(ballRigidbody.position));
        }
    }
}