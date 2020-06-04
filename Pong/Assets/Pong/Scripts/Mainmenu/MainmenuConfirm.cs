using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class MainmenuConfirm : MonoBehaviour, IMainmenuSelectable
    {
        public enum ConfirmMode : int
        {
            PvC, // PlayerVsCpu
            CvC, // CpuVsCpu
        };

        // ボタンに割り当てられた定数定義
        private static readonly int BackButton = 0;   // 戻る
        private static readonly int StartButton = 1;  // 開始

        private static readonly string PvCText = "PvC";
        private static readonly string CvCText = "CvC";
        private static readonly string CpuLevel1Text = "1";
        private static readonly string CpuLevel2Text = "2";

        private static readonly string ModeValueTextObjectName = "Canvas/ModeValueText";
        private static readonly string Cpu1LevelTextValueObjectName = "Canvas/Cpu1LevelValueText";
        private static readonly string Cpu2LevelTextValueObjectName = "Canvas/Cpu2LevelValueText";
        private static readonly string Cpu2LevelTextObjectName = "Canvas/Cpu2LevelText";


        // 戻るか
        private bool isSelectedBack = false;
        // 移動先のタイプ
        private MainmenuConstant.Type type;
        // 確認モード
        private MainmenuConfirm.ConfirmMode mode;
        // Cpu1レベル
        private Pong.BoardCpuLevel cpu1 = new Pong.BoardCpuLevel(BoardCpuLevel.Level.Level1);
        // Cpu2レベル
        private Pong.BoardCpuLevel cpu2 = new Pong.BoardCpuLevel(BoardCpuLevel.Level.Level1);


        public MainmenuConfirm.ConfirmMode Mode
        {
            set { mode = value; }
        }

        public Pong.BoardCpuLevel Cpu1
        {
            set { cpu1 = value; }
        }

        public Pong.BoardCpuLevel Cpu2
        {
            set { cpu2 = value; }
        }

        private void Start()
        {
            //mode = ConfirmMode.CvC;
            type = MainmenuConstant.Type.None;
            SetModeText(mode);
            SetCpuText(PlayerConstant.Type.Cpu1, cpu1);
            SetCpuText(PlayerConstant.Type.Cpu2, cpu2);
            if (mode == ConfirmMode.PvC)
            {
                SetCpu2TextEmpty();
            }
        }

        /// <summary>
        /// 戻るを選択したか
        /// </summary>
        /// <returns>trueなら戻るを選択した</returns>
        public bool IsSelectedBack()
        {
            return isSelectedBack;
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
            return type;
        }

        /// <summary>
        /// ボタン選択
        /// </summary>
        /// <param name="number">選択されたボタン</param>
        public void OnClick(int number)
        {
            Debug.Log(number);
            if (number == BackButton)
            {
                isSelectedBack = true;
                return;
            }
            if (number == StartButton)
            {
                type = MainmenuConstant.Type.Start;
                return;
            }
        }

        /// <summary>
        /// Cpu2のテキストに空文字を設定
        /// </summary>
        private void SetCpu2TextEmpty()
        {
            var t = transform.Find(Cpu2LevelTextObjectName).GetComponent<UnityEngine.UI.Text>();
            UnityEngine.Assertions.Assert.IsNotNull(t);
            t.text = "";
            var vt = transform.Find(Cpu2LevelTextValueObjectName).GetComponent<UnityEngine.UI.Text>();
            UnityEngine.Assertions.Assert.IsNotNull(vt);
            vt.text = "";
        }

        /// <summary>
        /// モードのテキストを設定
        /// </summary>
        /// <param name="mode">モード</param>
        private void SetModeText(MainmenuConfirm.ConfirmMode mode)
        {
            var t = transform.Find(ModeValueTextObjectName).GetComponent<UnityEngine.UI.Text>();
            UnityEngine.Assertions.Assert.IsNotNull(t);
            if (mode == MainmenuConfirm.ConfirmMode.PvC)
            {
                t.text = PvCText;
                return;
            }
            if (mode == MainmenuConfirm.ConfirmMode.CvC)
            {
                t.text = CvCText;
                return;
            }
        }

        /// <summary>
        /// Cpuのテキストを設定
        /// </summary>
        /// <param name="type">Cpuのタイプ</param>
        /// <param name="cpu">Cpuレベル</param>
        private void SetCpuText(Pong.PlayerConstant.Type type, Pong.BoardCpuLevel cpu)
        {
            // テキストを取得
            string objName = "";
            if (type == PlayerConstant.Type.Cpu1)
            {
                objName = Cpu1LevelTextValueObjectName;
            }
            if (type == PlayerConstant.Type.Cpu2)
            {
                objName = Cpu2LevelTextValueObjectName;
            }
            var t = transform.Find(objName).GetComponent<UnityEngine.UI.Text>();
            UnityEngine.Assertions.Assert.IsNotNull(t);
            // レベルに応じて設定
            if (cpu.Get() == BoardCpuLevel.Level.Level1)
            {
                t.text = CpuLevel1Text;
                return;
            }
            if (cpu.Get() == BoardCpuLevel.Level.Level2)
            {
                t.text = CpuLevel2Text;
                return;
            }
        }
    }
}
