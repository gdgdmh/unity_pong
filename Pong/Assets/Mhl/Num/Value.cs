using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mhl
{
    /// <summary>
    /// 値クラス
    /// </summary>
    public class Value
    {
        private int value; // 値

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Value()
        {
            value = 0;
        }

        /// <summary>
        /// 値の設定
        /// </summary>
        /// <param name="value">設定する値</param>
        public void Set(int value)
        {
            this.value = value;
        }

        /// <summary>
        /// 加算
        /// </summary>
        /// <param name="add">加算する値</param>
        public void Add(int add)
        {
            value += add;
        }

        /// <summary>
        /// 減算
        /// </summary>
        /// <param name="sub">減算する値</param>
        public void Sub(int sub)
        {
            value -= sub;
        }

        /// <summary>
        /// 値の取得
        /// </summary>
        /// <returns>値</returns>
        public int Get()
        {
            return value;
        }
    }
}