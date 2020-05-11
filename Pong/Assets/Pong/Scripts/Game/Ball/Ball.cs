using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class Ball : MonoBehaviour
    {
        public GameObject ball;
        public Rigidbody2D rbody;

        // Start is called before the first frame update
        void Start()
        {
            UnityEngine.Assertions.Assert.IsNotNull(ball);
            rbody = ball.GetComponent<Rigidbody2D>();
            UnityEngine.Assertions.Assert.IsNotNull(rbody);
            // 回転をロック
            //rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            // 左進行
            rbody.velocity = new Vector2(-2.0f, 0.0f);
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
                v.x = v.x * -1;
                v.y = v.y * -1;
                rbody.velocity = v;
            }
            else if (collision.tag == Pong.Tag.ToString(Pong.Tag.Unity.Wall))
            {
                Debug.Log("Ball(Wall)");
            }
        }
    }
}
