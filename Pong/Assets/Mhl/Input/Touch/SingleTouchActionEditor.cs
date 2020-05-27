using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mhl
{
    public class SingleTouchActionEditor : Mhl.ISingleTouchActionable
    {
        protected static readonly int CurrentFrame = 0;
        protected static readonly int Before1Frame = 1;
        protected static readonly int TouchInfoMax = 2;

        // タッチ情報配列[0]が最新[1]が前フレーム
        protected TouchInfo[] touchInfo;

        public SingleTouchActionEditor()
        {
            Initialize();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            touchInfo = new TouchInfo[TouchInfoMax];
            for (int i = 0; i < TouchInfoMax; ++i)
            {
                touchInfo[i] = new TouchInfo();
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update()
        {
            // 前の状態を保存
            touchInfo[Before1Frame].Copy(touchInfo[CurrentFrame]);

            TouchInfo.Status status = touchInfo[CurrentFrame].TouchStatus;
            switch (status)
            {
                case TouchInfo.Status.None:
                    UpdateNone();
                    break;
                case TouchInfo.Status.Began:
                    UpdateBegan();
                    break;
                case TouchInfo.Status.Moved:
                    UpdateMoved();
                    break;
                case TouchInfo.Status.Stationary:
                    UpdateStationary();
                    break;
                case TouchInfo.Status.Ended:
                    UpdateEnded();
                    break;
                case TouchInfo.Status.Canceled:
                    UpdateCanceled();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 更新時State.None処理
        /// </summary>
        private void UpdateNone()
        {
            // 押したりしていない状態で押されたらBeganへ移行
            if (Input.GetMouseButtonDown(0) == true)
            {
                touchInfo[CurrentFrame].TouchId = 0;
                touchInfo[CurrentFrame].Position = Input.mousePosition;
                touchInfo[CurrentFrame].TouchStatus = TouchInfo.Status.Began;
            }
            else
            {
                // デフォルト
                touchInfo[CurrentFrame].Clear();
            }
        }

        /// <summary>
        /// 更新時State.Began処理
        /// </summary>
        private void UpdateBegan()
        {
            // 位置を設定
            touchInfo[CurrentFrame].Position = Input.mousePosition;
            if (Input.GetMouseButton(0) == true)
            {
                if (touchInfo[CurrentFrame].EqualPosition(touchInfo[Before1Frame]))
                {
                    touchInfo[CurrentFrame].TouchStatus = TouchInfo.Status.Stationary;
                }
                else
                {
                    touchInfo[CurrentFrame].TouchStatus = TouchInfo.Status.Moved;
                }
            }
            else
            {
                // 持ち上げられたのでEndedへ
                touchInfo[CurrentFrame].TouchStatus = TouchInfo.Status.Ended;
            }
        }

        /// <summary>
        /// 更新時State.Moved処理
        /// </summary>
        private void UpdateMoved()
        {
            touchInfo[CurrentFrame].Position = Input.mousePosition;
            if (Input.GetMouseButton(0) == false)
            {
                // 持ち上げられたのでEndedへ
                touchInfo[CurrentFrame].TouchStatus = TouchInfo.Status.Ended;
            }
            else
            {
                // 移動してないならStationary 移動していたらMoved
                if (touchInfo[CurrentFrame].EqualPosition(touchInfo[Before1Frame]) == true)
                {
                    touchInfo[CurrentFrame].TouchStatus = TouchInfo.Status.Stationary;
                }
                else
                {
                    touchInfo[CurrentFrame].TouchStatus = TouchInfo.Status.Moved;
                }
            }
        }

        /// <summary>
        /// 更新時State.Stationary処理
        /// </summary>
        private void UpdateStationary()
        {
            touchInfo[CurrentFrame].Position = Input.mousePosition;
            if (Input.GetMouseButton(0) == false)
            {
                // 持ち上げられたのでEndedへ
                touchInfo[CurrentFrame].TouchStatus = TouchInfo.Status.Ended;
            }
            else
            {
                // 移動してないならStationary 移動していたらkMoved
                if (touchInfo[CurrentFrame].EqualPosition(touchInfo[Before1Frame]) == true)
                {
                    touchInfo[CurrentFrame].TouchStatus = TouchInfo.Status.Stationary;
                }
                else
                {
                    touchInfo[CurrentFrame].TouchStatus = TouchInfo.Status.Moved;
                }
            }
        }

        /// <summary>
        /// 更新時State.Ended処理
        /// </summary>
        private void UpdateEnded()
        {
            // 終わった後に押されたらBeganへ移行
            if (Input.GetMouseButton(0) == false)
            {
                // デフォルト状態に戻す
                touchInfo[CurrentFrame].Clear();
            }
            else
            {
                // タッチIDは0固定
                touchInfo[CurrentFrame].TouchId = 0;
                // 位置
                touchInfo[CurrentFrame].Position = Input.mousePosition;
                touchInfo[CurrentFrame].TouchStatus = TouchInfo.Status.Began;
            }
        }

        /// <summary>
        /// 更新時State.Canceled処理
        /// </summary>
        private void UpdateCanceled()
        {
            // Endedと共通
            UpdateEnded();
        }

        /// <summary>
        /// データのリセットを行う
        /// </summary>
        public void Reset()
        {
            int size = touchInfo.Length;
            for (int i = 0; i < size; ++i)
            {
                UnityEngine.Assertions.Assert.IsNotNull(touchInfo[i]);
                touchInfo[i].Clear();
            }
        }

        /// <summary>
        /// デバッグ用データの出力
        /// </summary>
        public void Print()
        {
            UnityEngine.Assertions.Assert.IsNotNull(touchInfo[CurrentFrame]);
            UnityEngine.Assertions.Assert.IsNotNull(touchInfo[Before1Frame]);
            touchInfo[CurrentFrame].Print();
            touchInfo[Before1Frame].Print();
        }

        /// <summary>
        /// デバッグ用のデータ出力
        /// 前回のフレームからタッチ状態から異なっていたら出力
        /// </summary>
        public void PrintDifference()
        {
            UnityEngine.Assertions.Assert.IsNotNull(touchInfo[CurrentFrame]);
            UnityEngine.Assertions.Assert.IsNotNull(touchInfo[Before1Frame]);
            if (touchInfo[CurrentFrame].Equals(touchInfo[Before1Frame]) == false)
            {
                touchInfo[CurrentFrame].Print();
            }
        }

        /// <summary>
        /// タッチが全くされていない状態か
        /// </summary>
        /// <returns>タッチが全くされていない状態ならtrue</returns>
        public bool IsTouchNone()
        {
            UnityEngine.Assertions.Assert.IsNotNull(touchInfo[CurrentFrame]);
            if (touchInfo[CurrentFrame].TouchStatus == TouchInfo.Status.None)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// タッチが開始された状態か
        /// </summary>
        /// <returns>タッチが開始された状態ならtrue</returns>
        public bool IsTouchBegan()
        {
            UnityEngine.Assertions.Assert.IsNotNull(touchInfo[CurrentFrame]);
            if (touchInfo[CurrentFrame].TouchStatus == TouchInfo.Status.Began)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// タッチをし続けていて移動中か
        /// </summary>
        /// <returns>タッチをし続けていて移動中ならtrue</returns>
        public bool IsTouchMoved()
        {
            UnityEngine.Assertions.Assert.IsNotNull(touchInfo[CurrentFrame]);
            if (touchInfo[CurrentFrame].TouchStatus == TouchInfo.Status.Moved)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// タッチをし続けていて移動していないか
        /// </summary>
        /// <returns>タッチをし続けていて移動していないならtrue</returns>
        public bool IsTouchStationary()
        {
            UnityEngine.Assertions.Assert.IsNotNull(touchInfo[CurrentFrame]);
            if (touchInfo[CurrentFrame].TouchStatus == TouchInfo.Status.Stationary)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// タッチが終了したか(タッチをして持ち上げた)
        /// </summary>
        /// <returns>タッチが終了したならtrue</returns>
        public bool IsTouchEnded()
        {
            UnityEngine.Assertions.Assert.IsNotNull(touchInfo[CurrentFrame]);
            if (touchInfo[CurrentFrame].TouchStatus == TouchInfo.Status.Ended)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// タッチがキャンセルされたか(タッチをして何らかの理由でキャンセルされた)
        /// </summary>
        /// <returns>タッチがキャンセルされたならtrue</returns>
        public bool IsTouchCanceled()
        {
            UnityEngine.Assertions.Assert.IsNotNull(touchInfo[CurrentFrame]);
            if (touchInfo[CurrentFrame].TouchStatus == TouchInfo.Status.Canceled)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ドラッグ動作をしているか
        /// </summary>
        /// <returns><c>true</c>ドラッグ動作をしている<c>false</c>ドラッグ動作をしていない</returns>
        public bool IsDragging()
        {
            UnityEngine.Assertions.Assert.IsNotNull(touchInfo[CurrentFrame]);
            TouchInfo.Status status = touchInfo[CurrentFrame].TouchStatus;
            switch (status)
            {
                case TouchInfo.Status.Moved:
                case TouchInfo.Status.Stationary:
                    return true;
            }
            return false;
        }

        /// <summary>
        /// ワールド座標を取得する
        /// </summary>
        /// <param name="camera">カメラ</param>
        /// <param name="position">変換前の座標</param>
        /// <returns>変換された座標</returns>
        public Vector3 GetWorldPosition(UnityEngine.Camera camera, Vector3 position)
        {
            return camera.ScreenToWorldPoint(position);
        }

        /// <summary>
        /// タッチ座標を取得する
        /// </summary>
        /// <param name="camera">カメラ</param>
        /// <returns>ワールドのタッチ座標</returns>
        public Vector3 GetTouchPosition(UnityEngine.Camera camera)
        {
            return GetWorldPosition(camera, touchInfo[CurrentFrame].Position);
        }

        /// <summary>
        /// システムから取得できる無加工のタッチ位置を取得する
        /// </summary>
        /// <returns></returns>
        public Vector3 GetRawTouchPosition()
        {
            return touchInfo[CurrentFrame].Position;
        }
    }
}