using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// ゴール監視インターフェース
    /// </summary>
    public interface IGoalObservable
    {
        void Goal(PlayerConstant.Position position);
    }
}
