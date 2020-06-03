using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class MainmenuTask : MonoBehaviour
    {
        private static readonly string GameScenePath = "Game";

        //private int scene;
        private Pong.IMainmenuSelectable mainmenu;
        private GameObject mainmenuGameObject = null;
        private Pong.MainmenuConstant.Type type;
        private Pong.BoardCpuLevel cpu = new Pong.BoardCpuLevel(BoardCpuLevel.Level.Level1);
        private Pong.BoardCpuLevel cpu1 = new Pong.BoardCpuLevel(BoardCpuLevel.Level.Level1);
        private Pong.BoardCpuLevel cpu2 = new Pong.BoardCpuLevel(BoardCpuLevel.Level.Level1);

        public MainmenuTask()
        {
            //type = Pong.MainmenuConstant.Type.None;
            //type = Pong.MainmenuConstant.Type.PlayerModeSelect;
            type = Pong.MainmenuConstant.Type.PvcCpuLevelSelect;
        }

        // Start is called before the first frame update
        void Start()
        {
            SetPrefab(type);
        }


        // Update is called once per frame
        void Update()
        {
            if (mainmenu == null)
            {
                return;
            }
            
            if (type == Pong.MainmenuConstant.Type.PlayerModeSelect)
            {
                UpdatePlayerSelectPrefab();
                return;
            }
            if (type == Pong.MainmenuConstant.Type.PvcCpuLevelSelect)
            {
                UpdatePvcCpuSelectPrefab();
                return;
            }
            if (type == Pong.MainmenuConstant.Type.CvcCpuLevelSelect1)
            {
                UpdateCvcCpuSelect1Prefab();
                return;
            }
            if (type == Pong.MainmenuConstant.Type.CvcCpuLevelSelect2)
            {
                UpdateCvcCpuSelect2Prefab();
                return;
            }
            if (type == Pong.MainmenuConstant.Type.ConfirmPlay)
            {
                UpdateConfirmPlayPrefab();
                return;
            }
        }

        private void SetPlayerSelectPrefab()
        {
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Mainmenu/mainmenu_player_select");
            UnityEngine.Assertions.Assert.IsNotNull(prefab);
            GameObject g = Instantiate(prefab);
            UnityEngine.Assertions.Assert.IsNotNull(g);

            mainmenu = (Pong.IMainmenuSelectable)g.GetComponent<Pong.MainmenuPlayerModeSelect>();
            UnityEngine.Assertions.Assert.IsNotNull(mainmenu);

            mainmenuGameObject = g;
        }

        private void UpdatePlayerSelectPrefab()
        {
            if (mainmenu.GetTransitionType() == MainmenuConstant.Type.PvcCpuLevelSelect)
            {
                // プレイヤー VS Cpu
                // Cpuレベル選択
                Debug.Log("PvC");
                SwitchPrefab(MainmenuConstant.Type.PvcCpuLevelSelect);
                return;
            }
            if (mainmenu.GetTransitionType() == MainmenuConstant.Type.CvcCpuLevelSelect1)
            {
                // CPU同士の対戦はまだ未実装
                Debug.Log("CvC");
                SwitchPrefab(mainmenu.GetTransitionType());
                return;
            }
        }

        private void SetPvcCpuSelectPrefab()
        {
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Mainmenu/mainmenu_select_cpu_level");
            UnityEngine.Assertions.Assert.IsNotNull(prefab);
            GameObject g = Instantiate(prefab);
            UnityEngine.Assertions.Assert.IsNotNull(g);

            Pong.MainmenuCpuLevelSelect select = g.GetComponent<Pong.MainmenuCpuLevelSelect>();
            UnityEngine.Assertions.Assert.IsNotNull(select);
            select.SelectMode = Pong.MainmenuCpuLevelSelect.Mode.PvC;

            mainmenu = (Pong.IMainmenuSelectable)select;
            UnityEngine.Assertions.Assert.IsNotNull(mainmenu);

            mainmenuGameObject = g;
        }

        private void UpdatePvcCpuSelectPrefab()
        {
            if (mainmenu.IsSelectedBack() == true)
            {
                Debug.Log("back");
                SwitchPrefab(Pong.MainmenuConstant.Type.PlayerModeSelect);
                return;
            }

            if (mainmenu.GetTransitionType() == MainmenuConstant.Type.ConfirmPlay)
            {
                Debug.Log("Confirm");
                cpu = GetMainmenuCpuLevel();
                Debug.Log(cpu.Get());
                SwitchPrefab(mainmenu.GetTransitionType());
                return;
            }
        }

        private void SetCvcCpuSelect1Prefab()
        {
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Mainmenu/mainmenu_select_cpu_level");
            UnityEngine.Assertions.Assert.IsNotNull(prefab);
            GameObject g = Instantiate(prefab);
            UnityEngine.Assertions.Assert.IsNotNull(g);

            Pong.MainmenuCpuLevelSelect select = g.GetComponent<Pong.MainmenuCpuLevelSelect>();
            UnityEngine.Assertions.Assert.IsNotNull(select);
            select.SelectMode = Pong.MainmenuCpuLevelSelect.Mode.CvC1;

            mainmenu = (Pong.IMainmenuSelectable)select;
            UnityEngine.Assertions.Assert.IsNotNull(mainmenu);

            mainmenuGameObject = g;
        }

        private void UpdateCvcCpuSelect1Prefab()
        {
            if (mainmenu.IsSelectedBack() == true)
            {
                Debug.Log("back");
                SwitchPrefab(Pong.MainmenuConstant.Type.PlayerModeSelect);
                return;
            }

            if (mainmenu.GetTransitionType() == Pong.MainmenuConstant.Type.CvcCpuLevelSelect2)
            {
                Debug.Log("select2");
                cpu1 = GetMainmenuCpuLevel();
                Debug.Log(cpu1.Get());
                SwitchPrefab(mainmenu.GetTransitionType());
                return;
            }
        }

        private void SetCvcCpuSelect2Prefab()
        {
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Mainmenu/mainmenu_select_cpu_level");
            UnityEngine.Assertions.Assert.IsNotNull(prefab);
            GameObject g = Instantiate(prefab);
            UnityEngine.Assertions.Assert.IsNotNull(g);

            Pong.MainmenuCpuLevelSelect select = g.GetComponent<Pong.MainmenuCpuLevelSelect>();
            UnityEngine.Assertions.Assert.IsNotNull(select);
            select.SelectMode = Pong.MainmenuCpuLevelSelect.Mode.CvC2;

            mainmenu = (Pong.IMainmenuSelectable)select;
            UnityEngine.Assertions.Assert.IsNotNull(mainmenu);

            mainmenuGameObject = g;

        }

        private void UpdateCvcCpuSelect2Prefab()
        {
            if (mainmenu.IsSelectedBack() == true)
            {
                Debug.Log("back");
                SwitchPrefab(Pong.MainmenuConstant.Type.CvcCpuLevelSelect1);
                return;
            }

            if (mainmenu.GetTransitionType() == Pong.MainmenuConstant.Type.ConfirmPlay)
            {
                Debug.Log("confirm");
                cpu2 = GetMainmenuCpuLevel();
                Debug.Log(cpu2.Get());
                SwitchPrefab(mainmenu.GetTransitionType());
                return;
            }

        }

        private void SetConfirmPlayPrefab()
        {
        }

        private void UpdateConfirmPlayPrefab()
        {
        }

        /// <summary>
        /// メニューのPrefabを設定する
        /// </summary>
        /// <param name="type"></param>
        private void SetPrefab(Pong.MainmenuConstant.Type type)
        {
            if (type == Pong.MainmenuConstant.Type.PlayerModeSelect)
            {
                SetPlayerSelectPrefab();
                return;
            }
            if (type == Pong.MainmenuConstant.Type.PvcCpuLevelSelect)
            {
                SetPvcCpuSelectPrefab();
                return;
            }
            if (type == Pong.MainmenuConstant.Type.CvcCpuLevelSelect1)
            {
                SetCvcCpuSelect1Prefab();
                return;
            }
            if (type == Pong.MainmenuConstant.Type.CvcCpuLevelSelect2)
            {
                SetCvcCpuSelect2Prefab();
                return;
            }
            if (type == Pong.MainmenuConstant.Type.ConfirmPlay)
            {
                SetConfirmPlayPrefab();
                return;
            }
        }

        /// <summary>
        /// Prefabの開放
        /// </summary>
        private void UnsetPrefab()
        {
            if (mainmenuGameObject.gameObject != null)
            {
                Destroy(mainmenuGameObject.gameObject);
            }
            mainmenuGameObject = null;
        }

        /// <summary>
        /// Prefabの切り替え
        /// </summary>
        /// <param name="nextType">次のPrefabのタイプ</param>
        private void SwitchPrefab(Pong.MainmenuConstant.Type nextType)
        {
            // 現在使用中のPrefabを開放
            UnsetPrefab();
            // 指定されたPrefabを設定
            type = nextType;
            SetPrefab(type);
        }

        /// <summary>
        /// メインメニューで選択されたCpuレベルの取得
        /// </summary>
        /// <returns>Cpuレベルパラメータクラス</returns>
        private Pong.BoardCpuLevel GetMainmenuCpuLevel()
        {
            // CPuレベルを取得
            UnityEngine.Assertions.Assert.IsNotNull(mainmenu);
            Pong.MainmenuCpuLevelSelect select = mainmenu as Pong.MainmenuCpuLevelSelect;
            UnityEngine.Assertions.Assert.IsNotNull(select);
            return select.Get();
        }

        private void ChangeSceneGame()
        {
            // イベント登録してシーン切り替え
            UnityEngine.SceneManagement.SceneManager.sceneLoaded
                += EventGameSceneLoaded;
            UnityEngine.SceneManagement.SceneManager.LoadScene(GameScenePath);

        }

        /// <summary>
        /// シーン読み込み後イベント
        /// </summary>
        /// <param name="next">次のシーン</param>
        /// <param name="mode"></param>
        private void EventGameSceneLoaded(UnityEngine.SceneManagement.Scene next,
            UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            //var script = GameObject.FindWithTag("GameManager").GetComponent<>();

            // イベント削除
            UnityEngine.SceneManagement.SceneManager.sceneLoaded
                -= EventGameSceneLoaded;
        }
    }
}
