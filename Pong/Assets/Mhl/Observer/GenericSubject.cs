using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mhl
{
    /// <summary>
    /// オブザーバージェネリック(テンプレート)クラス
    /// </summary>
    /// <typeparam name="Type"></typeparam>
    public class GenericSubject<Type>
    {
        // オブザーバーオブジェクト
        protected List<Type> observers = new List<Type>();
        // 削除リクエストオブザーバー
        protected List<Type> requestRemoveObservers = new List<Type>();

        /// <summary>
        /// オブザーバーの追加
        /// </summary>
        /// <param name="observer">追加するオブザーバー</param>
        public void Add(Type observer)
        {
            observers.Add(observer);
        }

        /// <summary>
        /// オブザーバーの削除
        /// </summary>
        /// <param name="observer">削除するオブザーバー</param>
        public void Remove(Type observer)
        {
            observers.Remove(observer);
        }

        /// <summary>
        /// オブザーバー削除のリクエスト
        /// リクエストされたオブザーバーは通知実行後に削除される
        /// </summary>
        /// <param name="observer">削除リクエストをするオブザーバー</param>
        public void RequestRemove(Type observer)
        {
            requestRemoveObservers.Add(observer);
        }

        /// <summary>
        /// 削除リクエストを実行
        /// </summary>
        public void ExecuteRequestRemove()
        {
            // 削除リクエストがあったオブザーバーを削除
            foreach (var requestObserver in requestRemoveObservers)
            {
                Remove(requestObserver);
            }
            requestRemoveObservers.Clear();
        }

        /// <summary>
        /// オブザーバーを全て削除(削除リクエスト含む)
        /// </summary>
        public void Clear()
        {
            observers.Clear();
            requestRemoveObservers.Clear();
        }
    }
}