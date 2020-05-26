using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class Ball : MonoBehaviour
    {
        private enum Direction : int
        {
            LeftUp,     // 左上
            LeftDown,   // 左下
            RightUp,    // 右上
            RightDown,  // 右下
            Max
        };

        public static readonly float StartSpeed = 2.0f; // 初期スピード
        public static readonly float MaxSpeed = 24.0f;  // 最大スピード
        public static readonly float AddSpeed = 2.0f;   // 1回で上がるスピード
        public static readonly float MaxAngle = 360.0f; // 360度
        public static readonly float HalfAngle = MaxAngle / 2; // 180度
        public static readonly float QuarterAngle = MaxAngle / 4; // 90度
        public static readonly int RandomAngleMin = -15; // 玉のランダム角度(最小)
        public static readonly int RandomAngleMax = 15;  // 玉のランダム角度(最大)


        public GameObject ball;
        public Rigidbody2D rbody;
        [SerializeField] private GoalSubject goalSubject = new GoalSubject();
        [SerializeField] private GameTask gameTask = null;
        private Mhl.IRandIntGeneratable rand = new Mhl.RandIntSystem();
        private float speed;
        private float moveAngle;

        public Ball()
        {
            speed = 0.0f;
            moveAngle = 0.0f;

        }

        // Start is called before the first frame update
        void Start()
        {
            // 関連付け
            UnityEngine.Assertions.Assert.IsNotNull(ball);
            rbody = ball.GetComponent<Rigidbody2D>();
            UnityEngine.Assertions.Assert.IsNotNull(rbody);
            // 初期スピード設定
            speed = StartSpeed;
            // 発射
            Shot();
        }

        // Update is called once per frame
        void Update()
        {
        }

        /// <summary>
        /// ボールの打ち出し
        /// </summary>
        public void Shot()
        {
            Direction rs = GetRandomDirection();
            rbody.velocity = GetDirectionToVelocity(rs);
        }

        public void SetGameTaskScript(GameTask task)
        {
            gameTask = task;
        }

        private Direction GetRandomDirection()
        {
            return (Direction)rand.GetRange(0, (int)Direction.Max - 1);
        }

        /// <summary>
        /// 方向から移動ベクトルを取得する
        /// </summary>
        /// <param name="rs">方向</param>
        /// <returns>移動ベクトル</returns>
        private Vector2 GetDirectionToVelocity(Direction rs)
        {
            switch (rs)
            {
                case Direction.LeftUp:
                    return GetMoveVelocity(125, speed);
                case Direction.LeftDown:
                    return GetMoveVelocity(225, speed);
                case Direction.RightUp:
                    return GetMoveVelocity(45, speed);
                case Direction.RightDown:
                    return GetMoveVelocity(325, speed);
                default:
                    // 方向がおかしい または case未定義
                    UnityEngine.Assertions.Assert.IsTrue(false);
                    return GetMoveVelocity(0, 0);
            }
        }

        /// <summary>
        /// 移動のベクトルを取得する
        /// </summary>
        /// <param name="angle">角度</param>
        /// <param name="speed">スピード</param>
        /// <returns>移動ベクトル</returns>
        private Vector2 GetMoveVelocity(float angle, float speed)
        {
            moveAngle = angle;
            return Mhl.Direction.GetVelocity2D(angle, speed);
        }

        /// <summary>
        /// ゴール監視の追加
        /// </summary>
        /// <param name="o">追加するオブジェクト</param>
        public void AddGoalObserver(IGoalObservable o)
        {
            goalSubject.Add(o);
        }

        /// <summary>
        /// ゴール監視削除リクエスト
        /// </summary>
        /// <param name="o">削除するオブジェクト</param>
        public void RequestRemoveObserver(IGoalObservable o)
        {
            goalSubject.RequestRemove(o);
        }

        /// <summary>
        /// 当たり判定
        /// </summary>
        /// <param name="collision">当たったオブジェクト</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Ball");
            if (collision.tag == Pong.Tag.ToString(Pong.Tag.Unity.Board))
            {
                Debug.Log("Ball(Board)");
                SpeedUp();
                BoundBoard();
            }
            else if (collision.tag == Pong.Tag.ToString(Pong.Tag.Unity.Wall))
            {
                Debug.Log("Ball(Wall)");
                BoundWall();
            }
            else if (collision.tag == Pong.Tag.ToString(Pong.Tag.Unity.GoalL))
            {
                Debug.Log("Ball(GoalR)");
                goalSubject.NotifyObservers(PlayerConstant.Position.Right);
            }
            else if (collision.tag == Pong.Tag.ToString(Pong.Tag.Unity.GoalR))
            {
                Debug.Log("Ball(GoalL)");
                goalSubject.NotifyObservers(PlayerConstant.Position.Left);
            }
        }

        /// <summary>
        /// スピードアップ処理
        /// </summary>
        private void SpeedUp()
        {
            speed += AddSpeed;
            if (speed > MaxSpeed)
            {
                speed = MaxSpeed;
            }
        }

        /// <summary>
        /// ボード用バウンド処理
        /// </summary>
        private void BoundBoard()
        {
            // 反射させて、少しランダムな角度を設定
            float angle = GetRandomAngle(HalfAngle - moveAngle);
            rbody.velocity = GetMoveVelocity(angle, speed);
        }

        /// <summary>
        /// 壁用バウンド処理
        /// </summary>
        private void BoundWall()
        {
            // 反射させる
            rbody.velocity = GetMoveVelocity(MaxAngle - moveAngle, speed);
        }

        /// <summary>
        /// 玉のランダムな角度を取得する
        /// </summary>
        /// <param name="angle">基準になる角度</param>
        /// <returns>玉の角度</returns>
        private float GetRandomAngle(float angle)
        {
            return CalcRandomAngle(angle, RandomAngleMin, RandomAngleMax);
        }

        /// <summary>
        /// ランダムな角度を計算する
        /// </summary>
        /// <param name="angle">基準の角度</param>
        /// <param name="rangeMin">最小ランダム値</param>
        /// <param name="rangeMax">最大ランダム値</param>
        /// <returns>ランダムな角度</returns>
        private float CalcRandomAngle(float angle, int rangeMin, int rangeMax)
        {
            int randomAngle = rand.GetRange(rangeMin, rangeMax);
            Debug.Log(string.Format("angle = {0} random = {1}", angle, angle + randomAngle));
            return angle + randomAngle;
        }
    }
}
