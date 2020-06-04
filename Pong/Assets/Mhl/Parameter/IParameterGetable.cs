using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mhl
{
    /// <summary>
    /// パラメータ受け渡しインターフェース
    /// </summary>
    /// <typeparam name="T">テンプレート</typeparam>
    public interface IParameterGetable<T>
    {
        /// <summary>
        /// パラメータ取得
        /// </summary>
        /// <returns>テンプレート</returns>
        T Get();
    }
}
