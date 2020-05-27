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
        /// <param name="board">自分の板情報</param>
        /// <param name="ball">ボール情報</param>
        /// <returns>移動した後の板の情報</returns>
        BoardInfo MoveBoard(BoardInfo board, BallInfo ball);
    }
}