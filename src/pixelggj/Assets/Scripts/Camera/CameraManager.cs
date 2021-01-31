using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class CameraManager : MonoBehaviour {

        public Camera cam;
        [SerializeField] Vector2 cameraSize;
        GameObject target;
        Vector2 borderStartPos;
        Vector2 borderSize;

        bool isLimited;

        void Awake() {
            cam = cam ?? GetComponent<Camera>();
            isLimited = false;
        }

        public void SetFollowing(GameObject target) {
            this.target = target;
            isLimited = false;
        }

        public void SetFollowingLimited(GameObject target, Vector2 borderStartPos, Vector2 borderSize) {
            this.target = target;
            this.borderStartPos = borderStartPos;
            this.borderSize = borderSize;
            isLimited = true;
            // print(target.transform.position + ", " + borderStartPos + ", " + borderSize + ", " + cameraSize);
        }

        void LateUpdate() {

            if (target == null) {
                return;
            }

            if (isLimited) {
                cam.FollowTargetLimited(false, target.transform.position, borderStartPos, borderSize, cameraSize, 1f * Time.deltaTime);
            } else {
                cam.FollowTarget(target.transform.position);
            }

        }
    }
}