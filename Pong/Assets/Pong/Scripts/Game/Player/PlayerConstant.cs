﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class PlayerConstant
    {
        public enum Position : int
        {
            Left,   // 左側プレイヤー
            Right   // 右側プレイヤー
        };

        public static readonly int Count = 2; // プレイヤーの人数
    }
}