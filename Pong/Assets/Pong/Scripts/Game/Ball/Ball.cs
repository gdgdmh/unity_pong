﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class Ball : MonoBehaviour
    {
        private enum Direction : int
        {
            LeftUp,     // 左上
            LeftDown,   // 左下
            RightUp,    // 右上
            RightDown,  // 右下
            Max
        };

        public static readonly float StartSpeed = 2.0f; // 初期スピード
        public static readonly float MaxSpeed = 24.0f;  // 最大スピード
        public static readonly float AddSpeed = 2.0f;   // 1回で上がるスピード

        public GameObject ball;
        public Rigidbody2D rbody;
        private GoalSubject goalSubject = new GoalSubject();
        private Mhl.IRandIntGeneratable rand = new Mhl.RandIntSystem();
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
            Direction rs = GetRandomDirection();
            rbody.velocity = GetDirectionToVelocity(rs);
        }

        private Direction GetRandomDirection()
        {
            return (Direction)rand.GetRange(0, (int)Direction.Max - 1);
        }

        /// <summary>
        /// 方向から移動ベクトルを取得する
        /// </summary>
        /// <param name="rs">方向</param>
        /// <returns>移動ベクトル</returns>
        private Vector2 GetDirectionToVelocity(Direction rs)
        {
            switch (rs)
            {
                case Direction.LeftUp:
                    return Mhl.Direction.GetVelocity2D(125, speed);
                case Direction.LeftDown:
                    return Mhl.Direction.GetVelocity2D(225, speed);
                case Direction.RightUp:
                    return Mhl.Direction.GetVelocity2D(45, speed);
                case Direction.RightDown:
                    return Mhl.Direction.GetVelocity2D(325, speed);
                default:
                    // 方向がおかしい または case未定義
                    UnityEngine.Assertions.Assert.IsTrue(false);
                    return new Vector2(0, 0);
            }
        }

        /// <summary>
        /// ゴール監視の追加
        /// </summary>
        /// <param name="o"></param>
        public void AddGoalObserver(IGoalObservable o)
        {
            goalSubject.AddObserver(o);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Ball");
            if (collision.tag == Pong.Tag.ToString(Pong.Tag.Unity.Board))
            {
                Debug.Log("Ball(Board)");
                SpeedUp();
                BoundBoard();
            }
            else if (collision.tag == Pong.Tag.ToString(Pong.Tag.Unity.Wall))
            {
                Debug.Log("Ball(Wall)");
                BoundWall();
            }
            else if (collision.tag == Pong.Tag.ToString(Pong.Tag.Unity.GoalL))
            {
                Debug.Log("Ball(GoalL)");
                goalSubject.NotifyObservers(PlayerConstant.Position.Left);
            }
            else if (collision.tag == Pong.Tag.ToString(Pong.Tag.Unity.GoalR))
            {
                Debug.Log("Ball(GoalR)");
                goalSubject.NotifyObservers(PlayerConstant.Position.Right);
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
        /// ボード用バウンド処理
        /// </summary>
        private void BoundBoard()
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
            rbody.velocity = v;
        }

        /// <summary>
        /// 壁用バウンド処理
        /// </summary>
        private void BoundWall()
        {
            Vector2 v = rbody.velocity;
            v.y *= -1;
            rbody.velocity = v;
        }
    }
}
