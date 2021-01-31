using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JackUtil {

    public static class RectTransformExtention {

        public static Vector2 ScreenToLocalPosition(this RectTransform rect, Vector2 screenPosition, bool isOverlay) {
            Vector2 outPos; 
            Camera cam = null;
            if (!isOverlay) {
                cam = Camera.main;
            }
            bool _isInside = RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPosition, cam, out outPos);
            return outPos;
        }
    }
}