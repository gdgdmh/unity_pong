using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mhl
{
    public static class Direction
    {
        /// <summary>
        /// 方向を取得
        /// </summary>
        /// <param name="angle">角度(弧度法)</param>
        /// <returns>ベクトル</returns>
        public static Vector3 GetDirection(float angle)
        {
            // 発射のベクトルに変換
            return new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad),
                0);
        }

        /// <summary>
        /// 方向を取得
        /// </summary>
        /// <param name="angle">角度(弧度法)</param>
        /// <returns>2次元ベクトル</returns>
        public static Vector2 GetDirection2D(float angle)
        {
            // 発射のベクトルに変換
            return new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );
        }

        /// <summary>
        /// 方向ベクトルを取得する
        /// </summary>
        /// <param name="angle">角度(弧度法)</param>
        /// <param name="speed">スピード</param>
        /// <returns>ベクトル</returns>
        public static Vector3 GetVelocity(float angle, float speed)
        {
            return GetDirection(angle) * speed;
        }

        /// <summary>
        /// 方向ベクトルを取得する
        /// </summary>
        /// <param name="angle">角度(弧度法)</param>
        /// <param name="speed">スピード</param>
        /// <returns>2次元ベクトル</returns>
        public static Vector2 GetVelocity2D(float angle, float speed)
        {
            return GetDirection2D(angle) * speed;
        }
    }
}