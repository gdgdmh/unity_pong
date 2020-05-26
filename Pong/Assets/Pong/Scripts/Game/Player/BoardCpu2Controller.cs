using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class BoardCpu2Controller : Pong.IBoardControllable
    {
        private static readonly float MoveSpeed = 0.12f; // 移動速度
        //private static readonly float SlowReflectionPositionX = 2.0f; // 遅い反応に使用する位置
        private static readonly float BoardMarginDivision = 6.0f; // ボードのマージンに使用する除算倍率
        private Pong.PlayerConstant.Position position;

        public BoardCpu2Controller(Pong.PlayerConstant.Position position)
        {
            this.position = position;
        }

        /// <summary>
        /// ボードの移動処理
        /// </summary>
        /// <param name="board">現在のボード情報</param>
        /// <param name="ball">現在のボール情報</param>
        /// <returns>移動した後のボード情報を返す</returns>
        public BoardInfo MoveBoard(BoardInfo board, BallInfo ball)
        {
            if (ball == null)
            {
                return board;
            }

            Vector3 boardPosition = board.Position;
            Vector3 ballPosition = ball.Position;
            float boardMargin = board.Height / BoardMarginDivision;

            if (IsAllowableRangePosition(ballPosition.y, boardPosition.y, boardMargin))
            {
                // 誤差含めて範囲内なら何もしない
                return board;
            }

            // 移動
            return Move(board, ballPosition, boardPosition);
        }

        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="board">ボード情報</param>
        /// <param name="ballPosition">ボール位置</param>
        /// <param name="boardPosition">ボード位置</param>
        /// <returns>移動後のボード情報</returns>
        private BoardInfo Move(BoardInfo board, Vector3 ballPosition, Vector3 boardPosition)
        {
            if (boardPosition.y > ballPosition.y)
            {
                boardPosition.y -= MoveSpeed;
                return new BoardInfo(board.Height, boardPosition);
            }
            if (boardPosition.y < ballPosition.y)
            {
                boardPosition.y += MoveSpeed;
                return new BoardInfo(board.Height, boardPosition);
            }
            return board;
        }

        /// <summary>
        /// 許容範囲内の位置にいるか
        /// </summary>
        /// <param name="ballPositionY">ボール位置</param>
        /// <param name="boardPositionY">ボード位置</param>
        /// <param name="margin">許容範囲</param>
        /// <returns></returns>
        private bool IsAllowableRangePosition(float ballPositionY, float boardPositionY, float margin)
        {
            // 誤差含めて範囲内なら何もしない
            if (Mathf.Abs(boardPositionY - ballPositionY) < margin)
            {
                return true;
            }
            return false;
        }

    }
}
