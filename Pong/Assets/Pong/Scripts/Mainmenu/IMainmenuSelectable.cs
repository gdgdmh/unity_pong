using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// メインメニューインターフェースクラス
    /// </summary>
    public interface IMainmenuSelectable
    {
        /// <summary>
        /// 戻るを選択したか
        /// </summary>
        /// <returns>trueなら戻るを選択した</returns>
        bool IsSelectedBack();

        /// <summary>
        /// シーンから戻るを選択したか
        /// </summary>
        /// <returns>trueならシーンから戻るを選択した</returns>
        bool IsSelectedBackScene();

        /// <summary>
        /// 遷移先のタイプを取得する
        /// </summary>
        /// <returns>メインメニュー遷移先(Noneのときは移動しない)</returns>
        MainmenuConstant.Type GetTransitionType();
    }
}
