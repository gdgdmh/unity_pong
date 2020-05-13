using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class PlayerScore
    {
        private Mhl.Value[] scores = new Mhl.Value[Pong.PlayerConstant.Count];

        public PlayerScore()
        {
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
    }
}
