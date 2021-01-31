using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rewired;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class KeyBindingWindow : PanelBase {

        protected override GameObject defaultSelectedGo => closeBtn.gameObject;

        [SerializeField] Button closeBtn;

        protected override void Start() {
            base.Start();
            closeBtn.onClick.AddListener(Close);
            Close();
        }

    }
}