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
        // プレイヤーのスコア
        private Pong.PlayerScore score = new PlayerScore();
        // シーン管理変数
        Scene scene;

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
        }

        private void SceneInitialize()
        {
            scene = Scene.ShotBall;
        }

        private void SceneShotBall()
        {
            CreateBall();
            AddGoalObserver(ballScript);
            scene = Scene.Playing;
        }

        private void ScenePlaying()
        {
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
            // ball delete
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

            scene = Scene.GoalSeStart;
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
    }
}