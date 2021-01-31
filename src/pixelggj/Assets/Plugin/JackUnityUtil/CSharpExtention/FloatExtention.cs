using System;

namespace JackUtil {

    public static class FloatExtention {

        public static float ToOne(this float f) {
            if (f > 0) {
                f = 1;
            } else if (f < 0) {
                f = -1;
            }
            return f;
        }

        public static float FloorToHalf(this float f) {
            float t = (float)Math.Floor(f);
            if (f > t + 0.5f) {
                return t + 0.5f;
            } else if (f < t + 0.5f) {
                return t;
            } else {
                return t;
            }
        }

        public static bool IsBetween(this float f, float targetValue, float range) {
            return f >= targetValue - range && f <= targetValue + range;
        }
    }
}