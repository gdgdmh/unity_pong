using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mhl
{
    /// <summary>
    /// 整数乱数生成(Systemメソッド使用)
    /// </summary>
    public class RandIntSystem : IRandIntGeneratable
    {
        private System.Random random; // 乱数生成クラス

        public RandIntSystem()
        {
            random = new System.Random();
        }

        /// <summary>
        /// 乱数の種を設定
        /// </summary>
        /// <param name="seed">設定する乱数の種</param>
        public void SetSeed(int seed)
        {
            UnityEngine.Assertions.Assert.IsNotNull(random);
            random = new System.Random(seed);
        }

        /// <summary>
        /// 範囲指定での乱数の取得
        /// </summary>
        /// <param name="min">取得乱数の最小値</param>
        /// <param name="max">取得乱数の最大値</param>
        /// <returns>整数値乱数(min <= n <= max)</returns>
        public int GetRange(int min, int max)
        {
            UnityEngine.Assertions.Assert.IsNotNull(random);
            int value = random.Next(max - min);
            value = value + min;
            UnityEngine.Assertions.Assert.IsTrue((value >= min) && (value <= max));
            return value;
        }

        /// <summary>
        /// 乱数の取得
        /// </summary>
        /// <returns>整数値乱数</returns>
        public int Get()
        {
            UnityEngine.Assertions.Assert.IsNotNull(random);
            return random.Next();
        }
    }
}