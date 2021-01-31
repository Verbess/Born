using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace JackUtil {

    public class ShakeFX : MonoBehaviour {

        Sequence shakeAction;
        Vector3 basePos;

        void Awake() {
            basePos = transform.position;
        }

        void Start() {
            StartShake();
        }

        public void StartShake() {
            shakeAction?.Kill();
            shakeAction = DOTween.Sequence();
            float t = UnityEngine.Random.Range(0.1f, 0.2f);
            shakeAction.Append(transform.DOMoveY(transform.position.y + t, 0.3f).SetEase(Ease.Linear));
            shakeAction.Append(transform.DOMoveY(transform.position.y, t).SetEase(Ease.Linear));
            shakeAction.Append(transform.DOMoveY(transform.position.y - t, 0.3f).SetEase(Ease.Linear));
            shakeAction.Append(transform.DOMoveY(transform.position.y, t).SetEase(Ease.Linear));
            shakeAction.SetLoops(-1);
        }

        public void StopShake() {
            shakeAction?.Kill();
            transform.position = basePos;
        }

        void OnDestroy() {
            StopShake();
        }

    }
}