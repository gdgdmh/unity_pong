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
        // カメラ(座標変換に必要)
        private UnityEngine.Camera camera;
        // 追従をするか
        private bool isTracingEnable = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position">操作対象プレイヤー</param>
        /// <param name="touchAction">タッチアクション</param>
        /// <param name="camera">カメラ</param>
        public BoardTouchController(Pong.PlayerConstant.Position position,
            Mhl.ISingleTouchActionable touchAction, UnityEngine.Camera camera)
        {
            this.position = position;
            this.touchAction = touchAction;
            this.camera = camera;
        }

        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="board">自分の板情報</param>
        /// <param name="ball">ボール情報</param>
        /// <returns>移動した後の板の情報</returns>
        public BoardInfo MoveBoard(BoardInfo board, BallInfo ball)
        {
            //Debug.Log(string.Format("board x = {0} y = {1} z = {2}",
            //    board.Position.x, board.Position.y, board.Position.z));
            //Debug.Log(string.Format("ball x = {0} y = {1} z = {2}",
            //    ball.Position.x, ball.Position.y, ball.Position.z));

            if (touchAction.IsDragging())
            {
                // タッチ中
                if (isTracingEnable)
                {
                    // タッチのY座標だけ追従する
                    Vector3 touchPosition = touchAction.GetTouchPosition(camera);
                    Vector3 boardPosition = board.Position;
                    boardPosition.y = touchPosition.y;
                    return new BoardInfo(board.Height, boardPosition);
                }
            }
            if (touchAction.IsTouchBegan())
            {
                Vector3 touchPosition = touchAction.GetTouchPosition(camera);
                // タッチを開始したときに範囲内ならバーの追従を開始
                if ((position == PlayerConstant.Position.Left)
                    && (touchPosition.x < 0))
                {
                    isTracingEnable = true;
                    return board;
                }
                if ((position == PlayerConstant.Position.Right)
                    && (touchPosition.x > 0))
                {
                    isTracingEnable = true;
                    return board;
                }
            }
            // タッチ終了、あるいはキャンセルなどされていたら追従を終了
            isTracingEnable = false;
            return board;
        }
    }
}