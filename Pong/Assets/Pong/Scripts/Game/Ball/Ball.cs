using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class Ball : MonoBehaviour
    {
        public static readonly float StartSpeed = 2.0f;
        public static readonly float MaxSpeed = 24.0f;
        public static readonly float AddSpeed = 2.0f;


        public GameObject ball;
        public Rigidbody2D rbody;
        private float speed;

        // Start is called before the first frame update
        void Start()
        {
            UnityEngine.Assertions.Assert.IsNotNull(ball);
            rbody = ball.GetComponent<Rigidbody2D>();
            UnityEngine.Assertions.Assert.IsNotNull(rbody);
            speed = StartSpeed;

            Shot();
        }

        // Update is called once per frame
        void Update()
        {
        }

        /// <summary>
        /// ボールの打ち出し
        /// </summary>
        public void Shot()
        {
            // 左進行
            rbody.velocity = new Vector2(-speed, speed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Ball");
            if (collision.tag == Pong.Tag.ToString(Pong.Tag.Unity.Board))
            {
                Debug.Log("Ball(Board)");
                SpeedUp();
                Bound();
            }
            else if (collision.tag == Pong.Tag.ToString(Pong.Tag.Unity.Wall))
            {
                Debug.Log("Ball(Wall)");
            }
            else if (collision.tag == Pong.Tag.ToString(Pong.Tag.Unity.GoalL))
            {
                Debug.Log("Ball(GoalL)");
            }
            else if (collision.tag == Pong.Tag.ToString(Pong.Tag.Unity.GoalR))
            {
                Debug.Log("Ball(GoalR)");
            }
        }

        /// <summary>
        /// スピードアップ処理
        /// </summary>
        private void SpeedUp()
        {
            speed += AddSpeed;
            if (speed > MaxSpeed)
            {
                speed = MaxSpeed;
            }
        }

        /// <summary>
        /// バウンド処理
        /// </summary>
        private void Bound()
        {
            Vector2 v = rbody.velocity;
            if (v.x > 0)
            {
                v.x = -speed;
            }
            else
            {
                v.x = speed;
            }
            v.y = v.y * -1;
            rbody.velocity = v;
            Debug.Log(rbody.velocity.x);
        }
    }
}
