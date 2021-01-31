using UnityEngine;

namespace JackUtil {

    public static class Collider2DExtention {

        public static Vector2 GetRightUpBorderPos(this Collider2D c) {
            SpriteRenderer sr = c.GetComponent<SpriteRenderer>();
            if (sr != null) {
                return (Vector2)c.transform.position + sr.size;
            } else {
                return (Vector2)c.transform.position + c.offset + (Vector2)c.bounds.size / 2;
            }
        }

    }
}