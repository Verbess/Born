using UnityEngine;

namespace JackUtil {

    public static class ColorCollection {

        public static Color titleColor;
        public static Color contentColor;

        static ColorCollection() {

            LoadPlanA();

        }

        public static void LoadPlanA() {

            contentColor = new Color32(255, 255, 255, 242);

        }

    }
}