using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class MainmenuConstant
    {
        public enum Type : int
        {
            None,               // なし
            PlayerModeSelect,   // プレイヤーモード選択
            PvcCpuLevelSelect,  // CPUレベル選択(PlayerVsCpu)
            CvcCpuLevelSelect1, // CPUレベル選択(CpuVsCpu1人目)
            CvcCpuLevelSelect2, // CPUレベル選択(CpuVsCpu2人目)
        };
    }
}
