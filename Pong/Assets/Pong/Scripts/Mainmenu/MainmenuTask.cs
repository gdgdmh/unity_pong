using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class MainmenuTask : MonoBehaviour
    {
        //private int scene;
        private Pong.IMainmenuSelectable mainmenu;
        private Pong.MainmenuConstant.Type type;

        public MainmenuTask()
        {
            //type = Pong.MainmenuConstant.Type.None;
            //type = Pong.MainmenuConstant.Type.PlayerModeSelect;
            type = Pong.MainmenuConstant.Type.PvcCpuLevelSelect;
        }

        // Start is called before the first frame update
        void Start()
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
        }

        private void UpdatePlayerSelectPrefab()
        {
            if (mainmenu.GetTransitionType() == MainmenuConstant.Type.None)
            {
                // 何もしない
                return;
            }

            if (mainmenu.GetTransitionType() == MainmenuConstant.Type.PvcCpuLevelSelect)
            {
                Debug.Log("PvC");
                return;
            }
            if (mainmenu.GetTransitionType() == MainmenuConstant.Type.CvcCpuLevelSelect1)
            {
                Debug.Log("CvC");
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
        }

        private void UpdatePvcCpuSelectPrefab()
        {
            if (mainmenu.GetTransitionType() == MainmenuConstant.Type.None)
            {
                // 何もしない
                return;
            }

            if (mainmenu.GetTransitionType() == MainmenuConstant.Type.ConfirmPlay)
            {
                Debug.Log("Confirm");
                return;
            }
        }

        private void SetCvcCpuSelect1Prefab()
        {
        }

        private void UpdateCvcCpuSelect1Prefab()
        {
        }

        private void SetCvcCpuSelect2Prefab()
        {
        }

        private void UpdateCvcCpuSelect2Prefab()
        {
        }

        private void SetConfirmPlayPrefab()
        {
        }

        private void UpdateConfirmPlayPrefab()
        {
        }
    }
}
