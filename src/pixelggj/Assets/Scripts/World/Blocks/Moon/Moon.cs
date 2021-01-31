using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class Moon : MonoBehaviour {

        CameraManager cameraManager => Container.Instance.cameraManager;

        [SerializeField] Transform lockWorldPos;
        Vector3 lockUIPos;

        void Awake() {
            lockUIPos = cameraManager.cam.WorldToScreenPoint(lockWorldPos.position);
        }

        void Update() {
            transform.position = cameraManager.cam.ScreenToWorldPoint(lockUIPos);
        }
    }
}