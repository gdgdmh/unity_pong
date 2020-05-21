using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// ボール情報
    /// </summary>
    public class BallInfo
    {
        private float radius;
        private Vector3 position;

        public float Radius
        {
            get { return radius; }
        }

        public Vector3 Position
        {
            get { return position; }
        }

        public BallInfo(float radius, Vector3 position)
        {
            this.radius = radius;
            this.position = position;
        }
    }
}