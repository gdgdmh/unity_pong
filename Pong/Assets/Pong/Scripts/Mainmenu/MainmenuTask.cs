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
            type = Pong.MainmenuConstant.Type.None;
            //type = Pong.MainmenuConstant.Type.PlayerModeSelect;
            //scene = 0;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (type == Pong.MainmenuConstant.Type.PlayerModeSelect)
            {
                SetPlayerSelectPrefab();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (mainmenu == null)
            {
                return;
            }
            UpdatePlayerSelectPrefab();

        }

        private void SetPlayerSelectPrefab()
        {
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Mainmenu/mainmenu_player_select");
            UnityEngine.Assertions.Assert.IsNotNull(prefab);
            GameObject g = Instantiate(prefab);
            UnityEngine.Assertions.Assert.IsNotNull(g);

            mainmenu = (Pong.IMainmenuSelectable)g.GetComponent<Pong.MainmenuPlayerModeSelect>();
            UnityEngine.Assertions.Assert.IsNotNull(g);
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
    }
}
