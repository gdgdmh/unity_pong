using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class MainmenuCpuLevelSelect : MonoBehaviour, IMainmenuSelectable
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

        // コンストラクタが使用できないのでプロパティを使用
        public MainmenuCpuLevelSelect.Mode SelectMode
        {
            set { mode = value; }
        }

        /// <summary>
        /// 開始
        /// </summary>
        public void Start()
        {
            type = MainmenuConstant.Type.None;
            isSelectedBack = false;

            // CPU選択テキストの取得
            GameObject g = GameObject.Find("CpuLevelText");
            UnityEngine.Assertions.Assert.IsNotNull(g);
            cpuSelectText = g.GetComponent<UnityEngine.UI.Text>();
            UnityEngine.Assertions.Assert.IsNotNull(cpuSelectText);
            // テキスト設定
            SetModeToText(mode);
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
                Debug.Log("back");
                isSelectedBack = true;
                return;
            }
            if (number == CpuLevel1)
            {
                Debug.Log("CpuLevel1");
                type = MainmenuConstant.Type.ConfirmPlay;
                return;
            }
            if (number == CpuLevel2)
            {
                Debug.Log("CpuLevel2");
                type = MainmenuConstant.Type.ConfirmPlay;
                return;
            }
        }

        /// <summary>
        /// モードからテキストを設定する
        /// </summary>
        /// <param name="mode">対象モード</param>
        private void SetModeToText(MainmenuCpuLevelSelect.Mode mode)
        {
            UnityEngine.Assertions.Assert.IsNotNull(cpuSelectText);
            if (mode == MainmenuCpuLevelSelect.Mode.PvC)
            {
                Debug.Log("PvC");
                cpuSelectText.text = PvCText;
                return;
            }
            if (mode == MainmenuCpuLevelSelect.Mode.CvC1)
            {
                Debug.Log("CvC1");
                cpuSelectText.text = CvC1Text;
                return;
            }
            if (mode == MainmenuCpuLevelSelect.Mode.CvC2)
            {
                Debug.Log("CvC2");
                cpuSelectText.text = CvC2Text;
                return;
            }
        }
    }
}
