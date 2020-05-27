using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// ボードコントローラー生成クラス
    /// </summary>
    public class BoardControllerFactory : Pong.IBoardControllerCreatable
    {
        private Pong.PlayerConstant.Position position;
        private Mhl.ISingleTouchActionable touchAction;
        private UnityEngine.Camera camera;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position">ボード(プレイヤー)位置</param>
        /// <param name="touchAction">タッチ情報</param>
        /// <param name="camera">カメラ情報</param>
        public BoardControllerFactory(Pong.PlayerConstant.Position position,
            Mhl.ISingleTouchActionable touchAction, UnityEngine.Camera camera)
        {
            this.position = position;
            this.touchAction = touchAction;
            this.camera = camera;
        }

        /// <summary>
        /// ボードコントローラー作成
        /// </summary>
        /// <param name="type">コントローラーのタイプ</param>
        /// <returns>作成したコントローラー</returns>
        public IBoardControllable Create(BoardControllerConstant.Type type)
        {
            switch (type)
            {
                case BoardControllerConstant.Type.Touch:
                    return new BoardTouchController(position, touchAction, camera);
                case BoardControllerConstant.Type.Cpu1:
                    return new BoardCpu1Controller(position);
                case BoardControllerConstant.Type.Cpu2:
                    return new BoardCpu2Controller(position);
            }
            return null;
        }
    }

}
