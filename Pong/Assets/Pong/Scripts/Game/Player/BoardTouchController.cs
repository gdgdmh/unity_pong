using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// ボードタッチ操作クラス
    /// </summary>
    public class BoardTouchController : IBoardControllable
    {
        // プレイヤーの位置
        private Pong.PlayerConstant.Position position;
        // タッチ情報(外部で更新処理などを行ってもらうので渡してもらう)
        private Mhl.ISingleTouchActionable touchAction;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position">操作対象プレイヤー</param>
        public BoardTouchController(Pong.PlayerConstant.Position position,
            Mhl.ISingleTouchActionable touchAction)
        {
            this.position = position;
            this.touchAction = touchAction;
        }

        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="board">自分の板情報</param>
        /// <param name="ball">ボール情報</param>
        /// <returns>移動した後の板の情報</returns>
        public BoardInfo MoveBoard(BoardInfo board, BallInfo ball)
        {
            Debug.Log(string.Format("board x = {0} y = {1} z = {2}",
                board.Position.x, board.Position.y, board.Position.z));
            Debug.Log(string.Format("ball x = {0} y = {1} z = {2}",
                ball.Position.x, ball.Position.y, ball.Position.z));

            return board;
        }

        private void Update()
        {
            if (touchAction.IsDragging())
            {
                // タッチ中か
            }
            else if (touchAction.IsTouchBegan())
            {
                // タッチを開始したときに範囲内ならバーの追従を開始
            }
        }

    }
}