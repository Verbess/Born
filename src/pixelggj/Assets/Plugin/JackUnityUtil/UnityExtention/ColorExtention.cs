using System;
using System.Collections.Generic;
using UnityEngine;

namespace JackUtil {

    public static class ColorExtention {

        public static Color GetTransparent(this Color c) {
            c = new Color(c.r, c.g, c.a, 0);
            return c;
        }

        public static Color GetFullColor(this Color c) {
            c = new Color(c.r, c.g, c.a, 255);
            return c;
        }

        public static Color SetTransparent(this Color c, float a) {
            return new Color(c.r, c.g, c.b, a);
        }

        public static Color TendTo(this Color32 c, Color32 target, byte range) {
            return TendTo(c, target, (float)range/255f);
        }

        public static Color TendTo(this Color c, Color target, float range) {
            if (c.r < target.r) {
                c.r += range;
                if (c.r >= target.r) {
                    c.r = target.r;
                }
            } else if (c.r > target.r) {
                c.r -= range;
                if (c.r <= target.r) {
                    c.r = target.r;
                }
            }

            if (c.g < target.g) {
                c.g += range;
                if (c.g >= target.g) {
                    c.g = target.g;
                }
            } else if (c.g > target.g) {
                c.g -= range;
                if (c.g <= target.g) {
                    c.g = target.g;
                }
            }

            if (c.b < target.b) {
                c.b += range;
                if (c.b >= target.b) {
                    c.b = target.b;
                }
            } else if (c.b > target.b) {
                c.b -= range;
                if (c.b <= target.b) {
                    c.b = target.b;
                }
            }

            if (c.a < target.a) {
                c.a += range;
                if (c.a >= target.a) {
                    c.a = target.a;
                }
            } else if (c.a > target.a) {
                c.a -= range;
                if (c.a <= target.a) {
                    c.a = target.a;
                }
            }

            return c;
        }
    }
}