using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JackUtil {

    public static class JUIStyle {

        public static Color bordBackgroundColorA = Color.white;
        public static Color fontNormalColorA = new Color32(112, 96, 80, 255);
        public static Color fontAlertColorA = new Color32(231, 81, 1, 255);
        public static Color fontHealthyColorA = new Color32(163, 206, 39, 255);

        public static Color bordBackgroundColorDefalut;
        public static Color fontNormalColorDefalut;
        public static Color fontAlertColorDefalut;
        public static Color fontHealthyColorDefalut;

        static JUIStyle() {
            SetCustom();
            bordBackgroundColorDefalut = bordBackgroundColorA;
            fontNormalColorDefalut = fontNormalColorA;
            fontAlertColorDefalut = fontAlertColorA;
            fontHealthyColorDefalut = fontHealthyColorA;
        }

        public static void SetCustom() {

            fontNormalColorA = new Color32(167, 181, 207, 255);
            fontAlertColorA = new Color32(167, 181, 207, 255);
            fontHealthyColorA = new Color32(167, 181, 207, 255);

        }

    }
}