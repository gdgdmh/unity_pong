using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class GameRuleTenPoint : IGameRulable
    {
        private static readonly int EndScore = 10;

        /// <summary>
        /// ゲームが終了したかチェック
        /// </summary>
        /// <param name="score">スコア</param>
        /// <returns>trueなら終了</returns>
        public bool CheckEnd(Pong.PlayerScore score)
        {
            if (score.Get(Pong.PlayerConstant.Position.Left) >= EndScore)
            {
                return true;
            }
            if (score.Get(Pong.PlayerConstant.Position.Right) >= EndScore)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 勝ったプレイヤーの取得
        /// </summary>
        /// <param name="score">スコア</param>
        /// <returns>勝ったプレイヤー</returns>
        public Pong.PlayerConstant.Position GetWinner(Pong.PlayerScore score)
        {
            UnityEngine.Assertions.Assert.IsTrue(CheckEnd(score));
            if (score.Get(Pong.PlayerConstant.Position.Left) > score.Get(Pong.PlayerConstant.Position.Right))
            {
                return PlayerConstant.Position.Left;
            }
            return PlayerConstant.Position.Right;
        }
    }
}