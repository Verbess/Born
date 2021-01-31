using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JackUtil;

namespace PixelGGJNS {

    public class GameOverPanel : PanelBase {

        protected override GameObject defaultSelectedGo => panelBtn.gameObject;

        [SerializeField] Button panelBtn;

        protected override void Awake() {
            base.Awake();
            panelBtn.onClick.AddListener(Close);
        }

        protected override void Start() {
            base.Start();
            Close();
        }

    }
}