﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mhl
{
    public interface ISingleTouchActionable
    {
        /// <summary>
        /// 初期化
        /// </summary>
        void Initialize();

        /// <summary>
        /// 更新処理
        /// (毎フレーム処理する)
        /// </summary>
        void Update();

        /// <summary>
        /// データのリセットを行う
        /// シーン移動などで以前のデータが残らないようにする
        /// </summary>
        void Reset();

        /// <summary>
        /// デバッグ用データの出力
        /// </summary>
        void Print();

        /// <summary>
        /// デバッグ用のデータ出力
        /// 前回のフレームからタッチ状態から異なっていたら出力
        /// </summary>
        void PrintDifference();

        /// <summary>
        /// タッチが全くされていない状態か
        /// </summary>
        /// <returns>タッチが全くされていない状態ならtrue</returns>
        bool IsTouchNone();

        /// <summary>
        /// タッチが開始された状態か
        /// </summary>
        /// <returns>タッチが開始された状態ならtrue</returns>
        bool IsTouchBegan();

        /// <summary>
        /// タッチをし続けていて移動中か
        /// </summary>
        /// <returns>タッチをし続けていて移動中ならtrue</returns>
        bool IsTouchMoved();

        /// <summary>
        /// タッチをし続けていて移動していないか
        /// </summary>
        /// <returns>タッチをし続けていて移動していないならtrue</returns>
        bool IsTouchStationary();

        /// <summary>
        /// タッチが終了したか(タッチをして持ち上げた)
        /// </summary>
        /// <returns>タッチが終了したならtrue</returns>
        bool IsTouchEnded();

        /// <summary>
        /// タッチがキャンセルされたか(タッチをして何らかの理由でキャンセルされた)
        /// </summary>
        /// <returns>タッチがキャンセルされたならtrue</returns>
        bool IsTouchCanceled();

        /// <summary>
        /// ドラッグ動作をしているか
        /// </summary>
        /// <returns><c>true</c>ドラッグ動作をしている<c>false</c>ドラッグ動作をしていない</returns>
        bool IsDragging();

        /// <summary>
        /// タッチ位置を取得する
        /// </summary>
        /// <returns>座標</returns>
        Vector3 GetTouchPosition();

        /// <summary>
        /// システムから取得できる無加工のタッチ位置を取得する
        /// </summary>
        /// <returns></returns>
        Vector3 GetRawTouchPosition();
    }
}