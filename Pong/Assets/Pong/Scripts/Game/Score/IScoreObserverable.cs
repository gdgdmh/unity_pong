using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// スコア通知インターフェース
    /// </summary>
    public interface IScoreObserverable
    {
        void Update(Pong.PlayerScore score);
    }
}