﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong {
    public class GoalSubject
    {
        private List<IGoalObservable> observers = new List<IGoalObservable>();

        /// <summary>
        /// 通知の追加
        /// </summary>
        /// <param name="o">通知を追加するゴール監視オブジェクト</param>
        public void AddObserver(IGoalObservable o)
        {
            observers.Add(o);
        }

        /// <summary>
        /// 通知の削除
        /// </summary>
        /// <param name="o">通知を削除するオブジェクト</param>
        public void RemoveObserver(IGoalObservable o)
        {
            observers.Remove(o);
        }

        /// <summary>
        /// 通知を全て削除する
        /// </summary>
        public void ClearObserver()
        {
            observers.Clear();
        }

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
        }
    }
}