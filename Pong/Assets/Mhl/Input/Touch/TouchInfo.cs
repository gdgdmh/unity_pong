using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mhl
{
    /// <summary>
    /// タッチ情報クラス
    /// </summary>
    public class TouchInfo
    {
        /// <summary>
        /// タッチ状態定義
        /// </summary>
        public enum Status
        {
            None,       // 何もなし
            Began,      // タッチ開始
            Moved,      // ドラッグ中
            Stationary, // ドラッグ中で移動なし
            Ended,      // タッチ終了
            Canceled    // キャンセル
        };
        private static readonly int InvalidTouchId = -1; // 無効なタッチID


        // タッチID
        private int touchId = InvalidTouchId;
        // タッチ位置
        private Vector3 position = new Vector3(0, 0, 0);
        // ステータス
        private Status status = Status.None;

        public int TouchId
        {
            set { touchId = value; }
            get { return touchId; }
        }
        public Vector3 Position
        {
            set { position = value; }
            get { return position; }
        }
        public Status TouchStatus
        {
            set { status = value; }
            get { return status; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TouchInfo()
        {
            Clear();
        }

        /// <summary>
        /// データをクリア
        /// </summary>
        public void Clear()
        {
            touchId = InvalidTouchId;
            position = Vector3.zero;
            status = Status.None;
        }

        /// <summary>
        /// コピー
        /// </summary>
        /// <param name="source">コピー元</param>
        public void Copy(TouchInfo source)
        {
            touchId = source.touchId;
            position = source.position;
            status = source.status;
        }

        /// <summary>
        /// 位置が一致しているかチェック
        /// </summary>
        /// <param name="source">比較元</param>
        /// <returns>trueなら位置が一致している</returns>
        public bool EqualPosition(TouchInfo source)
        {
            if ((position.x == source.position.x)
                && (position.y == source.position.y)
                && (position.z == source.position.z))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// データが一致しているか
        /// </summary>
        /// <param name="source">比較元</param>
        /// <returns>trueならデータが一致している</returns>
        public bool Equals(TouchInfo source)
        {
            if ((touchId == source.touchId)
                && (position.x == source.position.x)
                && (position.y == source.position.y)
                && (position.z == source.position.z)
                && (status == source.status))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// タッチIDが無効か
        /// </summary>
        /// <returns>trueならタッチIDが無効なデータである</returns>
        public bool IsTouchIdInvalid()
        {
            return IsTouchIdInvalid(touchId);
        }

        /// <summary>
        /// 指定されたタッチIDが無効か
        /// </summary>
        /// <param name="id">チェックするタッチID</param>
        /// <returns>trueならタッチIDが無効</returns>
        public static bool IsTouchIdInvalid(int id)
        {
            if (id == InvalidTouchId)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ステータスを文字列に変換する
        /// </summary>
        /// <param name="touchStatus">タッチ状態</param>
        /// <returns>タッチ状態の文字列</returns>
        public static string ToStatusString(TouchInfo.Status touchStatus)
        {
            switch (touchStatus)
            {
                case Status.None:
                    return "None";
                case Status.Began:
                    return "Began";
                case Status.Moved:
                    return "Moved";
                case Status.Stationary:
                    return "Stationaly";
                case Status.Ended:
                    return "Ednded";
                case Status.Canceled:
                    return "Canceled";
                default:
                    return "Unknown";
            }
        }

        /// <summary>
        /// データを表示
        /// </summary>
        public void Print()
        {
            Debug.Log(string.Format("id={0} x={1} y={2} z={3} status={4}",
                touchId, position.x, position.y, position.z, ToStatusString(status)));
        }
    }
}