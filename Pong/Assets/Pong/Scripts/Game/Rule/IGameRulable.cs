using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// ゲームルールインターフェース
    /// </summary>
    public interface IGameRulable
    {
        /// <summary>
        /// ゲームが終了したかチェック
        /// </summary>
        /// <param name="score">スコア</param>
        /// <returns>trueなら終了</returns>
        bool CheckEnd(Pong.PlayerScore score);

        /// <summary>
        /// 勝ったプレイヤーの取得
        /// </summary>
        /// <param name="score">スコア</param>
        /// <returns>勝ったプレイヤー</returns>
        Pong.PlayerConstant.Position GetWinner(Pong.PlayerScore score);
    }
}