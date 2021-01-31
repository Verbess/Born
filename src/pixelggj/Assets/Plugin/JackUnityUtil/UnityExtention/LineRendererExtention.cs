using System;
using UnityEngine;
using UnityEngine.UI;

namespace JackUtil {

    public static class LineRendererExtention {

        public static void DrawSquare(this LineRenderer lr, Vector2 startPos, Vector2 endPos, Material material, Color color, float weight) {

            lr.startColor = color;
            lr.endColor = color;

            lr.startWidth = weight;
            lr.endWidth = weight;

            lr.material = material;

            lr.positionCount = 5;
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, new Vector3(startPos.x, endPos.y));
            lr.SetPosition(2, endPos);
            lr.SetPosition(3, new Vector3(endPos.x, startPos.y));
            lr.SetPosition(0, startPos);
        }

        public static void DrawHollowCircle(this LineRenderer lr, Camera camera, Vector3 mousePosition, Color color, float border, float radius) {

            lr.startColor = color;
            lr.endColor = color;

            lr.startWidth = border;
            lr.endWidth = border;

            int pointCount = 362;
            lr.positionCount = pointCount;
            
            Vector3 targetPosition = camera.GetMouseWorldPosition(mousePosition);

            // 以鼠标为中心画圆
            for (int i = 0; i < pointCount; i += 1) {

                float x = Mathf.Cos((360 * (i + 1) / pointCount) * Mathf.Deg2Rad) * radius + targetPosition.x;
                float y = Mathf.Sin((360 * (i + 1) / pointCount) * Mathf.Deg2Rad) * radius + targetPosition.y;
                lr.SetPosition(i, new Vector2(x, y));
                
            }

        }

        public static void DrawSolidCircle(this LineRenderer lr, Camera camera, Vector3 mousePosition, Color color, float radius) {

            lr.startColor = color;
            lr.endColor = color;

            lr.startWidth = radius;
            lr.endWidth = radius;

            lr.positionCount = 2;

            Vector3 targetPosition = camera.GetMouseWorldPosition(mousePosition);

            lr.SetPosition(0, new Vector2(targetPosition.x, targetPosition.y));
            lr.SetPosition(1, new Vector2(targetPosition.x, targetPosition.y));

        }

        public static void DrawSolidRay(this LineRenderer lr, Camera camera, Vector3 fromPosition, Vector3 mousePosition, Color color, float width) {

            lr.startColor = color;
            lr.endColor = color;

            lr.startWidth = width;
            lr.endWidth = width;

            lr.positionCount = 2;

            Vector3 targetPosition = camera.GetMouseWorldPosition(mousePosition);
            lr.SetPosition(0, new Vector2(fromPosition.x, fromPosition.y));
            lr.SetPosition(1, new Vector2(targetPosition.x, targetPosition.y));

        }

        public static void DrawSolidRay(this LineRenderer lr, Camera camera, Vector3 fromPosition, Vector3 mousePosition, Color color, float width, LayerMask layer) {

            lr.startColor = color;
            lr.endColor = color;

            lr.startWidth = width;
            lr.endWidth = width;

            lr.positionCount = 2;

            Vector3 targetPosition = camera.GetMouseWorldPosition(mousePosition);
            RaycastHit2D hit = Physics2D.Linecast(fromPosition, targetPosition, layer);
            if (hit) {
                targetPosition = hit.point;
            }
            lr.SetPosition(0, new Vector2(fromPosition.x, fromPosition.y));
            lr.SetPosition(1, new Vector2(targetPosition.x, targetPosition.y));

        }

    }
}