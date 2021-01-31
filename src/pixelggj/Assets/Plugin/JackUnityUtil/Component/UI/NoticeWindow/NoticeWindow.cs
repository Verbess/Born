using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JackUtil {

    public class NoticeWindow : PanelBase {

        public Text contentText;
        [SerializeField] Button windowButton;

        protected override GameObject defaultSelectedGo => null;

        protected override void Awake() {
            base.Awake();
            windowButton.onClick.AddListener(() => {
                Close();
            });
        }

        protected override void Start() {
            base.Start();
            Close();
        }

        public void Init(string content) {
            this.contentText.text = content;
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            windowButton.onClick.RemoveAllListeners();
        }

    }

}