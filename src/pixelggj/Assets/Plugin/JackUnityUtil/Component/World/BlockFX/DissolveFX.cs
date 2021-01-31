using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace JackUtil {

    public class DissolveFX : MonoBehaviour {

        SpriteRenderer sr;
        float dissolveValue;
        Sequence dissolveAction;

        void Awake() {
            sr = GetComponent<SpriteRenderer>();
            dissolveValue = 1;
        }

        public void DissolveIfHasTheShader(float stepTime, Action onCompleted = null) {
            dissolveAction?.Kill();
            dissolveAction = DOTween.Sequence();
            dissolveAction.AppendInterval(Time.deltaTime);
            dissolveAction.AppendCallback(() => {
                dissolveValue -= stepTime;
                if (dissolveValue <= 0) {
                    sr.material.SetFloat("_Fade", 0);
                    dissolveAction?.Complete();
                } else {
                    sr.material.SetFloat("_Fade", dissolveValue);
                }
            });
            dissolveAction.SetLoops(-1);
            dissolveAction.onComplete = () => onCompleted?.Invoke();

        }

        void OnDestroy() {
            dissolveAction?.Kill();
        }

    }
}