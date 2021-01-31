using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class Container : Singleton<Container> {

        public DataManager data;
        public CameraManager cameraManager;
        public WorldManager world;
        public UIManager ui;
        public AudioManager audioManager;

        public SpriteCollection spriteCollection;

        protected override void Awake() {
            base.Awake();
        }

    }

}