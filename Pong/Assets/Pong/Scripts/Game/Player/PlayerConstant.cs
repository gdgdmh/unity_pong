using System.Collections;
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

        public enum Type : int
        {
            Man,    // 人間
            Cpu     // CPU
        };

        public static readonly int Count = 2; // プレイヤーの人数
    }
}
