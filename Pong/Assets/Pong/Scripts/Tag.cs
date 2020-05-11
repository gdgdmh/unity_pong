using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// UnityのTag定義クラス
    /// </summary>
    public static class Tag
    {
        // タグ定義
        public enum Unity : int
        {
            Ball,   // 玉
            Board,  // 板
            Wall,   // 4方の壁
        };
        /// <summary>
        /// タグ定義から文字列に変換
        /// </summary>
        /// <param name="tag">タグ定義</param>
        /// <returns>タグから変換された文字列</returns>
	    public static string ToString(Tag.Unity tag)
        {
            // 今は一時変数として定義(参照が多いならstatic変数にすることも考慮)
            string[] names =
            {
                "ball",
                "board",
                "wall"
            };
            return names[(int)tag];
        }
    }
}
