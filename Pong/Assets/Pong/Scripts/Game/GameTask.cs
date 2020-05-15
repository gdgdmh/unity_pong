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
            Scored,     // 加点処理
            Ended       // 終了
        };

        // ballオブジェクト
        public GameObject ball;
        // ballスクリプト
        private Pong.Ball ballScript;
        // プレイヤーのスコア
        private Pong.PlayerScore score = new PlayerScore();

        //Scene scene;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GameTask()
        {
            //scene = Scene.Initialize;
        }

        // Start is called before the first frame update
        void Start()
        {
            CreateBall();
            AddGoalObserver(ballScript);
        }

        // Update is called once per frame
        void Update()
        {
        }

        /// <summary>
        /// ゴールイベント
        /// </summary>
        /// <param name="position">ゴールした方のプレイヤー</param>
        public void Goal(PlayerConstant.Position position)
        {
            // ball delete
            RemoveGoalObserver(ballScript);
            Destroy(ball);
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

            CreateBall();
            AddGoalObserver(ballScript);
        }

        private void CreateBall()
        {
            Pong.BallFactory factory = new Pong.BallFactory();
            ball = factory.Create();
            Instantiate(ball, new Vector3(0, 0, 0), Quaternion.identity);
            UnityEngine.Assertions.Assert.IsNotNull(ball);
            ballScript = ball.GetComponent<Ball>();
            UnityEngine.Assertions.Assert.IsNotNull(ballScript);
        }

        private void AddGoalObserver(Pong.Ball balScript)
        {
            ballScript.AddGoalObserver(this);
        }

        private void RemoveGoalObserver(Pong.Ball ballScript)
        {
            ballScript.RemoveGoalObserver(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
        }
    }
}