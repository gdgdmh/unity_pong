using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// スコアサブジェクトクラス
    /// </summary>
    public class ScoreSubject : Mhl.GenericSubject<IScoreObserverable>
    {
        /// <summary>
        /// スコアを通知する
        /// </summary>
        /// <param name="score">スコアデータ</param>
        public void NotifyObservers(Pong.PlayerScore score)
        {
            foreach (var observer in observers)
            {
                observer.Update(score);
            }
            ExecuteRequestRemove();
        }
    }
}