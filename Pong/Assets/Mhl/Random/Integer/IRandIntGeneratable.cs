using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mhl
{
    /// <summary>
    /// 整数値乱数生成インターフェース
    /// </summary>
    interface IRandIntGeneratable
    {
        /// <summary>
        /// 乱数の種を設定
        /// </summary>
        /// <param name="seed">設定する乱数の種</param>
        void SetSeed(int seed);

        /// <summary>
        /// 範囲指定での乱数の取得
        /// </summary>
        /// <param name="min">取得乱数の最小値</param>
        /// <param name="max">取得乱数の最大値</param>
        /// <returns>整数値乱数(min <= n <= max)</returns>
        int GetRange(int min, int max);

        /// <summary>
        /// 乱数の取得
        /// </summary>
        /// <returns>整数値乱数</returns>
        int Get();
    }
}