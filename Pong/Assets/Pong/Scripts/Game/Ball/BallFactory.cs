using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class BallFactory
    {
        private readonly string Path = "Prefabs/Game/Ball/";
        private readonly string BallName = "ball";

        public BallFactory()
        {
        }

        public GameObject Create()
        {
            GameObject g = (GameObject)Resources.Load(Path + BallName);
            UnityEngine.Assertions.Assert.IsNotNull(g);
            return g;
        }
    }
}