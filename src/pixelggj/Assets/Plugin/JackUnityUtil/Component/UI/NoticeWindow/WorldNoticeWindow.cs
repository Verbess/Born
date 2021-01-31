using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace JackUtil {

    public class WorldNoticeWindow : NoticeWindow, IWorldPanel {

        public Canvas worldCanvas;
        bool isSet = false;

        protected override void Start() {
            base.Start();
            Close();
        }

        public void Init(string content, Vector3 pos) {
            this.contentText.text = content;
            transform.position = pos;
        }

        public virtual void SetCanvas(Canvas uiCanvas, Camera camera) {
            worldCanvas.WorldScaleToUICanvas(uiCanvas, camera);
            isSet = true;
        }

        void Update() {
            if (!isSet) {
                DebugHelper.LogError("需要初始化 -> 请调用 SetCanvas 方法");
                isSet = true;
            }
        }

    }
}