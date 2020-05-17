using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class ScoreTask : MonoBehaviour, Pong.IScoreObserverable
    {
        public GameObject textGameObject = null;
        public GameObject gameTaskObject = null;
        private UnityEngine.UI.Text text = null;

        public ScoreTask()
        {
        }

        // Start is called before the first frame update
        void Start()
        {
            // textの関連付け
            UnityEngine.Assertions.Assert.IsNotNull(textGameObject);
            text = textGameObject.GetComponent<UnityEngine.UI.Text>();
            UnityEngine.Assertions.Assert.IsNotNull(text);
            // score関連付け
            UnityEngine.Assertions.Assert.IsNotNull(gameTaskObject);
            GameTask task = gameTaskObject.GetComponent<GameTask>();
            UnityEngine.Assertions.Assert.IsNotNull(task);
            task.AddScoreObserver(this);
        }

        // Update is called once per frame
        void Update()
        {
        }

        /// <summary>
        /// スコア更新
        /// </summary>
        /// <param name="score">スコアデータ</param>
        void IScoreObserverable.Update(PlayerScore score)
        {
            text.text = string.Format("{0} - {1}",
                score.Get(PlayerConstant.Position.Left),
                score.Get(PlayerConstant.Position.Right));
        }
    }
}