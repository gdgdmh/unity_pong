using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// ボード操作インターフェース
    /// </summary>
    public interface IBoardControllable
    {
        /// <summary>
        /// 移動
        /// </summary>
        void MoveBoard();
    }
}