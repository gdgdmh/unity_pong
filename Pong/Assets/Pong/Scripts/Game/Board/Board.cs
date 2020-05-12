﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
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

        /// <summary>
        /// 当たり判定(触れた瞬間)
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Board");
            if (collision.tag == Pong.Tag.ToString(Pong.Tag.Unity.Ball))
            {
                Debug.Log("Board(Ball)");
            }
        }
    }
}