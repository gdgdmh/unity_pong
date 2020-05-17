using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class ScoreTask : MonoBehaviour
    {
        public GameObject textGameObject = null;
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
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}