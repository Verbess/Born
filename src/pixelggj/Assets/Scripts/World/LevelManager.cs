using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JackUtil;

namespace PixelGGJNS {

    public class LevelManager : MonoBehaviour {

        public Dictionary<string, MapGo> mapDic { get; private set; }
        [SerializeField] MapGo C1L1;
        [SerializeField] MapGo C1L2;

        void Awake() {
            mapDic = new Dictionary<string, MapGo>();
            mapDic.Add(C1L1.levelId, C1L1);
            mapDic.Add(C1L2.levelId, C1L2);
        }
    }
}