using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// 板情報
    /// </summary>
    public class BoardInfo
    {
        private float height; // 板の縦幅
        private Vector3 position; // 板の位置

        public float Height
        {
            get { return height; }
        }
        public Vector3 Position
        {
            get { return position; }
        }

        public BoardInfo(float height, Vector3 position)
        {
            this.height = height;
            this.position = position;
        }
    }
}