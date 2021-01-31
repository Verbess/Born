using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

namespace JackUtil {

    public static class CanvasExtention {

        public static Vector2 ScreenToLocalPosition(this Canvas _canvas, Vector2 _screenPosition, Camera _uiCamera) {
            RectTransform _rect =_canvas.GetComponent<RectTransform>();
            Vector2 _pos;
            bool _isInside = RectTransformUtility.ScreenPointToLocalPointInRectangle(_rect, _screenPosition, _uiCamera, out _pos);
            return _pos;
        }

        public static void WorldScaleToUICanvas(this Canvas canvas, Canvas uiCanvas, Camera camera) {
            PixelPerfectCamera perfectCamera = camera.GetComponent<PixelPerfectCamera>();
            if (perfectCamera == null) {
                DebugHelper.LogError("不存在: PixelPerfectCamera");
                return;
            }
            Vector3 v3 = new Vector3((float)perfectCamera.assetsPPU / (float)perfectCamera.refResolutionX, (float)perfectCamera.assetsPPU / (float)perfectCamera.refResolutionX, 1);
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.worldCamera = camera;
            RectTransform r = canvas.GetComponent<RectTransform>();
            Vector3 offScale = uiCanvas.GetComponent<RectTransform>().localScale;
            if (offScale.x == 0 || offScale.y == 0) {
                r.localScale = v3;
            } else {
                r.localScale = new Vector3(v3.x / offScale.x, v3.y / offScale.y, 1);
            }
        }

    }
}