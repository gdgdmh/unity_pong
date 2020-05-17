using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong {
    /// <summary>
    /// ゴール通知サブジェクトクラス
    /// </summary>
    public class GoalSubject : Mhl.GenericSubject<IGoalObservable>
    {
        /// <summary>
        /// ゴールを通知する
        /// </summary>
        /// <param name="position"></param>
        public void NotifyObservers(PlayerConstant.Position position)
        {
            foreach (var observer in observers)
            {
                observer.Goal(position);
            }
            // 削除リクエストがあったオブザーバーを削除
            foreach (var requestObserver in requestRemoveObservers)
            {
                Remove(requestObserver);
            }
        }
    }
}