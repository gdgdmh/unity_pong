using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// プレイヤーのスコアを管理する
    /// </summary>
    public class PlayerScore
    {
        private Mhl.Value[] scores = new Mhl.Value[Pong.PlayerConstant.Count];

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PlayerScore()
        {
            // new
            for (int i = 0; i < scores.Length; ++i)
            {
                scores[i] = new Mhl.Value();
            }

            // 0clear
            foreach (Mhl.Value s in scores)
            {
                s.Set(0);
            }
        }

        /// <summary>
        /// 1点加算する
        /// </summary>
        /// <param name="position">加算するプレイヤー</param>
        public void Add(Pong.PlayerConstant.Position position)
        {
            UnityEngine.Assertions.Assert.AreEqual(scores.Length, PlayerConstant.Count);
            scores[(int)position].Add(1);
        }

        /// <summary>
        /// プレイヤーのスコアを取得
        /// </summary>
        /// <param name="position">取得対象プレイヤー</param>
        /// <returns>プレイヤーのスコア</returns>
        public int Get(Pong.PlayerConstant.Position position)
        {
            UnityEngine.Assertions.Assert.AreEqual(scores.Length, PlayerConstant.Count);
            return scores[(int)position].Get();
        }
    }
}
