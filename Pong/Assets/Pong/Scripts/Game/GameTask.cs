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
            UnityEngine.Assertions.Assert.IsNotNull(ball);
            ballScript = ball.GetComponent<Ball>();
            UnityEngine.Assertions.Assert.IsNotNull(ballScript);
            ballScript.AddGoalObserver(this);

            //scene = Scene.Initialize;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void Goal(PlayerConstant.Position position)
        {
            if (position == PlayerConstant.Position.Left)
            {
                Debug.Log("GameTask(Goal) Left");
            }
            else
            {
                Debug.Log("GameTask(Goal) Right");
            }
        }

        private void ShotRandom()
        {
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
        }
    }
}