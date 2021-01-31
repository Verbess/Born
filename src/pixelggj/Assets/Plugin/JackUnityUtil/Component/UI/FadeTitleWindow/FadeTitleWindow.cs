using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace JackUtil {

    public class FadeTitleWindow : MonoBehaviour {

        [SerializeField] Text content;
        Sequence action;

        protected void Start() {
            Close();
        }

        public void Init(string text) {
            content.text = text;
        }

        public void Close() {
            this.Hide();
            action?.Kill();
        }

        public void FadeInOut(float stayTime, float outDuration) {

            this.Show();
            content.color = content.color.SetTransparent(1);

            action?.Kill();
            action = DOTween.Sequence();
            action.AppendInterval(stayTime);
            action.Append(content.DOFade(0, outDuration));
            action.AppendCallback(() => {
                Close();
            });

        }

        void OnDestroy() {
            action?.Kill();
        }
    }
}