using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class MainmenuPlayerModeSelect : MonoBehaviour, IMainmenuSelectable
    {
        private static readonly int PlayerVsCpu = 0;
        private static readonly int CpuVsCpu = 1;

        private MainmenuConstant.Type select;

        public MainmenuPlayerModeSelect()
        {
            select = MainmenuConstant.Type.None;
        }

        /// <summary>
        /// 戻るを選択したか
        /// </summary>
        /// <returns>trueなら戻るを選択した</returns>
        public bool IsSelectedBack()
        {
            return false;
        }

        /// <summary>
        /// シーンから戻るを選択したか
        /// </summary>
        /// <returns>trueならシーンから戻るを選択した</returns>
        public bool IsSelectedBackScene()
        {
            return false;
        }

        /// <summary>
        /// 遷移先のタイプを取得する
        /// </summary>
        /// <returns>メインメニュー遷移先(Noneのときは移動しない)</returns>
        public MainmenuConstant.Type GetTransitionType()
        {
            return select;
        }

        public void OnClick(int number)
        {
            if (number == PlayerVsCpu)
            {
                select = MainmenuConstant.Type.PvcCpuLevelSelect;
                return;
            }
            if (number == CpuVsCpu)
            {
                select = MainmenuConstant.Type.CvcCpuLevelSelect1;
                return;
            }
        }

    }
}
