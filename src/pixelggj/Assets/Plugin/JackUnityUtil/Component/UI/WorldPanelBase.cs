using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JackUtil {

    public abstract class WorldPanelBase : PanelBase {

        public Canvas worldCanvas;

        bool isSet = false;

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