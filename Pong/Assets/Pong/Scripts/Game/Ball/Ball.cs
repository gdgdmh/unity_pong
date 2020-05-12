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
            // 左進行
            rbody.velocity = new Vector2(-speed, 0.0f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetVelocity(Vector2 velocity)
        {
            rbody.velocity = new Vector2(velocity.x, velocity.y);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Ball");
            if (collision.tag == Pong.Tag.ToString(Pong.Tag.Unity.Board))
            {
                Debug.Log("Ball(Board)");
                Vector2 v = rbody.velocity;
                speed += AddSpeed;
                if (speed >= MaxSpeed)
                {
                    speed = MaxSpeed;
                }
                if (v.x > 0)
                {
                    v.x = -speed;
                }
                else
                {
                    v.x = speed;
                }
                //v.x = speed * -1;
                v.y = v.y * -1;
                rbody.velocity = v;
                Debug.Log(rbody.velocity.x);
            }
            else if (collision.tag == Pong.Tag.ToString(Pong.Tag.Unity.Wall))
            {
                Debug.Log("Ball(Wall)");
            }
        }
    }
}
