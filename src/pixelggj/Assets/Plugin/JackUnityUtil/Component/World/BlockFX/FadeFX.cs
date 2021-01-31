using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace JackUtil {

    public class FadeFX : MonoBehaviour {

        SpriteRenderer sr;

        Sequence fadeAction;
        Sequence reshowAction;

        void Awake() {
            sr = GetComponent<SpriteRenderer>();
        }

        public void Fade(float fadeOutTime, bool isDestroy = false, float fadeValue = 0, Action fadeCB = null) {

            if (fadeAction != null) {

                fadeAction.Kill();

            }

            fadeAction = DOTween.Sequence();

            if (sr == null) {

                sr = GetComponent<SpriteRenderer>();

            }

            fadeAction.Append(sr.DOFade(fadeValue, fadeOutTime));
            fadeAction.AppendCallback(() => {

                fadeCB?.Invoke();

                if (isDestroy) {

                    Destroy(gameObject);

                } else {

                    gameObject.SetActive(false);

                }
                
            });

        }

        public void ReShow(float fadeOutTime, float reShowTime, Action fadeCB = null, Action showCD = null) {

            if (reshowAction != null) {

                reshowAction.Kill();

            }

            reshowAction = DOTween.Sequence();

            if (sr == null) {

                sr = GetComponent<SpriteRenderer>();

            }

            reshowAction.Append(sr.DOFade(0, fadeOutTime));
            reshowAction.AppendCallback(() => {
                fadeCB?.Invoke();
                gameObject.SetActive(false);
            });
            reshowAction.AppendInterval(reShowTime);
            reshowAction.AppendCallback(() => {
                showCD?.Invoke();
                gameObject.SetActive(true);
                sr.color = Color.white;
                reshowAction = null;
            });

        }

        void OnDestroy() {

            reshowAction?.Kill();
            fadeAction?.Kill();

        }

    }

}