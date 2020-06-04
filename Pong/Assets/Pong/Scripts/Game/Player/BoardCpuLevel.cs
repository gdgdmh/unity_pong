using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class BoardCpuLevel
    {
        // Cpuレベル
        public enum Level : int
        {
            Level1,
            Level2
        };

        private Level level; // Cpuレベル

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BoardCpuLevel(Level level)
        {
            this.level = level;
        }

        /// <summary>
        /// Cpuレベルの取得
        /// </summary>
        /// <returns>Cpuレベル</returns>
        public BoardCpuLevel.Level Get()
        {
            return level;
        }
    }
}
