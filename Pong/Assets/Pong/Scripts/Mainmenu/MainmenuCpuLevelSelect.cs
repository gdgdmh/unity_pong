using System.Collections;
using System.Collections.Generic;
using Mhl;
using UnityEngine;

namespace Pong
{
    public class MainmenuCpuLevelSelect : MonoBehaviour, IMainmenuSelectable, Mhl.IParameterGetable<Pong.BoardCpuLevel>
    {
        // Cpu選択のモード
        public enum Mode : int
        {
            PvC,    // PlayerVsCpu(Cpu1体のみ)
            CvC1,   // CpuVsCpu(1体目)
            CvC2    // CpuVsCpu(2体目)
        };

        // ボタンに割り当てられた定数定義
        private static readonly int Back = 0;       // 戻る
        private static readonly int CpuLevel1 = 1;  // CpuLevel1
        private static readonly int CpuLevel2 = 2;  // CpuLevel2
        // CPU選択で表示されるテキスト
        private static readonly string PvCText = "CPUレベル選択";
        private static readonly string CvC1Text = "CPUレベル選択(1人目)";
        private static readonly string CvC2Text = "CPUレベル選択(2人目)";

        // CPU選択モード(いくつかパターンがあるのでモードとして分ける)
        private MainmenuCpuLevelSelect.Mode mode;
        // 移動先のタイプ
        private MainmenuConstant.Type type;
        // CPU選択で出すテキスト
        private UnityEngine.UI.Text cpuSelectText = null;
        // 戻るか
        private bool isSelectedBack = false;
        // Cpuレベル
        private BoardCpuLevel level = new BoardCpuLevel(BoardCpuLevel.Level.Level1);
        // Startメソッドが呼ばれたか
        private bool IsStartCalled = false;

        // コンストラクタが使用できないのでプロパティを使用
        // Startが呼ばれた後は変更しないこと
        public MainmenuCpuLevelSelect.Mode SelectMode
        {
            set
            {
                // Startが呼ばれた後にModeを変更するのは許可しない
                UnityEngine.Assertions.Assert.IsFalse(IsStartCalled);
                mode = value;
            }
        }

        /// <summary>
        /// 開始
        /// </summary>
        public void Start()
        {
            type = MainmenuConstant.Type.None;
            isSelectedBack = false;

            // CPU選択テキストの取得
            GameObject g = transform.Find("Canvas/CpuLevelText").gameObject;
            UnityEngine.Assertions.Assert.IsNotNull(g);
            cpuSelectText = g.GetComponent<UnityEngine.UI.Text>();
            UnityEngine.Assertions.Assert.IsNotNull(cpuSelectText);
            // テキスト設定
            SetModeToText(mode);
            IsStartCalled = true;
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
            if (number == Back)
            {
                // 戻るボタン
                Debug.Log("back");
                isSelectedBack = true;
                return;
            }
            if (number == CpuLevel1)
            {
                // Level1ボタン
                Debug.Log("CpuLevel1");
                level = new BoardCpuLevel(BoardCpuLevel.Level.Level1);
                type = GetClickCpuLevel1Type(mode);
                return;
            }
            if (number == CpuLevel2)
            {
                // Level2ボタン
                Debug.Log("CpuLevel2");
                level = new BoardCpuLevel(BoardCpuLevel.Level.Level2);
                type = GetClickCpuLevel2Type(mode);
                return;
            }
        }

        private MainmenuConstant.Type GetClickCpuLevel1Type(MainmenuCpuLevelSelect.Mode mode)
        {
            if (mode == Mode.CvC1)
            {
                return MainmenuConstant.Type.CvcCpuLevelSelect2;
            }
            return MainmenuConstant.Type.ConfirmPlay;
        }

        private MainmenuConstant.Type GetClickCpuLevel2Type(MainmenuCpuLevelSelect.Mode mode)
        {
            if (mode == Mode.CvC1)
            {
                return MainmenuConstant.Type.CvcCpuLevelSelect2;
            }
            return MainmenuConstant.Type.ConfirmPlay;
        }


        /// <summary>
        /// モードからテキストを設定する
        /// </summary>
        /// <param name="mode">対象モード</param>
        private void SetModeToText(MainmenuCpuLevelSelect.Mode setMode)
        {
            UnityEngine.Assertions.Assert.IsNotNull(cpuSelectText);
            if (mode == MainmenuCpuLevelSelect.Mode.PvC)
            {
                Debug.Log("PvC text");
                cpuSelectText.text = PvCText;
                return;
            }
            if (mode == MainmenuCpuLevelSelect.Mode.CvC1)
            {
                Debug.Log("CvC1 text");
                cpuSelectText.text = CvC1Text;
                return;
            }
            if (mode == MainmenuCpuLevelSelect.Mode.CvC2)
            {
                Debug.Log("CvC2 text");
                cpuSelectText.text = CvC2Text;
                return;
            }
        }

        /// <summary>
        /// パラメータの取得
        /// </summary>
        /// <returns>Cpuレベルパラメータ</returns>
        public BoardCpuLevel Get()
        {
            return level;
        }
    }
}
