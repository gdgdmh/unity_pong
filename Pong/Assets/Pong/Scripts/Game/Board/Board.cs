using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject board;
    public Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Assertions.Assert.IsNotNull(board);
        rbody = board.GetComponent<Rigidbody2D>();
        UnityEngine.Assertions.Assert.IsNotNull(rbody);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D c)
	{
        Debug.Log("Board");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Debug.Log("BoardT");
    }

}
