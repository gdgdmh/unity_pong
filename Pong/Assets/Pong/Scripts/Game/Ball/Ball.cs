using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
		// 左進行
        rbody.velocity = new Vector2(-2.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D c)
	{
        Debug.Log("Ball");
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
        Debug.Log("BallT");
    }
}
