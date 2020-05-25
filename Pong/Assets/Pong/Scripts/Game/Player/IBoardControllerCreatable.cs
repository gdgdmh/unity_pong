using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// ボードコントローラー作成インターフェース
    /// </summary>
    public interface IBoardControllerCreatable
    {
        /// <summary>
        /// ボードコントローラー作成
        /// </summary>
        /// <param name="type">コントローラーのタイプ</param>
        /// <returns>作成したコントローラー</returns>
        Pong.IBoardControllable Create(Pong.BoardControllerConstant.Type type);
    }
}