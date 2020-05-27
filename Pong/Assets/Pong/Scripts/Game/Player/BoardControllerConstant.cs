using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// ボードコントローラー定数定義クラス
    /// </summary>
    public static class BoardControllerConstant
    {
        // ボードコントローラーのタイプ
        public enum Type : int
        {
            Touch,  // タッチ操作
            Cpu1,   // CPU1
            Cpu2    // CPU2
        };
    }
}
