using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// ボードCPULv1操作クラス
    /// </summary>
    public class BoardCpu1Controller : Pong.IBoardControllable
    {
        private static readonly float MoveSpeed = 0.01f;

        public BoardInfo MoveBoard(BoardInfo board, BallInfo ball)
        {
            if (ball == null)
            {
                return board;
            }

            Vector3 boardPosition = board.Position;
            Vector3 ballPosition = ball.Position;
            float boardMargin = board.Height / 2;

            // CPUが右にいる前提で打たれてすぐ動かない
            if (ballPosition.x < -2.0f)
            {
                return board;
            }

            // 誤差含めて範囲内なら何もしない
            if (Mathf.Abs(boardPosition.y - ballPosition.y) < boardMargin)
            {
                return board;
            }

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
    }
}