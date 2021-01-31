using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace JackUtil {

    public class CurtainWindow : PanelBase {

        public Image curtain;

        Sequence action;

        protected override GameObject defaultSelectedGo => null;

        protected override void Start() {
            base.Start();
            Close();
        }

        public override void Execute() {

        }

        public override void Close() {
            base.Close();
            action?.Kill();
        }

        public void PlayFan(float closeTime = 0.5f, float waitTime = 0.2f, float openTime = 0.4f) {

            action?.Kill();

            action = DOTween.Sequence();
            action.AppendCallback(() => {
                curtain.type = Image.Type.Filled;
                curtain.fillMethod = Image.FillMethod.Radial180;
                curtain.fillOrigin = (int)Image.Origin180.Bottom;
                curtain.fillAmount = 0;
                curtain.fillClockwise = true;
            });
            action.Append(curtain.DOFillAmount(1, closeTime));
            action.AppendInterval(waitTime);
            action.AppendCallback(() => {
                // curtain.fillClockwise = false;
            });
            action.Append(curtain.DOFillAmount(0, openTime));
            action.AppendCallback(() => {
                Close();
            });

        }

        public void PlayFade(float fadeInTime, float maintain, float fadeOutTime) {
            action?.Kill();
            action = DOTween.Sequence();
            curtain.type = Image.Type.Simple;
            curtain.color = curtain.color.SetTransparent(0);
            action.Append(curtain.DOFade(1, fadeInTime));
            action.AppendInterval(maintain);
            action.Append(curtain.DOFade(0, fadeOutTime));
            action.AppendCallback(() => {
                Close();
            });
        }

        public void PlayNinja(Action callback) {

        }

        public void LeftToRight(float _closeTime = 1.5f, float _waitTime = 1.5f, float _openTime = 1f) {

            curtain.type = Image.Type.Filled;
            curtain.fillMethod = Image.FillMethod.Horizontal;
            curtain.fillOrigin = (int)Image.OriginHorizontal.Left;

            action = DOTween.Sequence();
            action.Append(curtain.DOFillAmount(1, _closeTime));
            action.AppendInterval(_waitTime);
            action.AppendCallback(() => {
                curtain.fillOrigin = (int)Image.OriginHorizontal.Right;
            });
            action.Append(curtain.DOFillAmount(0, _openTime));
            action.AppendCallback(() => {
                Close();
            });

        }

        public void RightToLeft(Action _callback = null, float _closeTime = 1.5f, float _waitTime = 1.5f, float _openTime = 1f) {

            curtain.type = Image.Type.Filled;

            curtain.fillMethod = Image.FillMethod.Horizontal;

            curtain.fillOrigin = (int)Image.OriginHorizontal.Right;

            action = DOTween.Sequence();

            action.Append(curtain.DOFillAmount(1, _closeTime));
            action.AppendInterval(_waitTime);
            action.AppendCallback(() => {
                curtain.fillOrigin = (int)Image.OriginHorizontal.Left;
                curtain.DOFillAmount(0, _openTime);
                _callback?.Invoke();
            });

        }

        public void TopToBottom(Action _callback = null, float _closeTime = 1.5f, float _waitTime = 1.5f, float _openTime = 1f) {

            curtain.type = Image.Type.Filled;

            curtain.fillMethod = Image.FillMethod.Vertical;

            curtain.fillOrigin = (int)Image.OriginVertical.Top;

            action = DOTween.Sequence();

            action.Append(curtain.DOFillAmount(1, _closeTime));
            action.AppendInterval(_waitTime);
            action.AppendCallback(() => {
                curtain.fillOrigin = (int)Image.OriginVertical.Bottom;
                curtain.DOFillAmount(0, _openTime);
                _callback?.Invoke();
            });

        }

        public void BottomToTop(Action _callback = null, float _closeTime = 1.5f, float _waitTime = 1.5f, float _openTime = 1f) {

            curtain.type = Image.Type.Filled;

            curtain.fillMethod = Image.FillMethod.Vertical;

            curtain.fillOrigin = (int)Image.OriginVertical.Bottom;

            action = DOTween.Sequence();

            action.Append(curtain.DOFillAmount(1, _closeTime));
            action.AppendInterval(_waitTime);
            action.AppendCallback(() => {
                curtain.fillOrigin = (int)Image.OriginVertical.Top;
                curtain.DOFillAmount(0, _openTime);
                _callback?.Invoke();
            });

        }

        protected override void OnDestroy() {

            base.OnDestroy();

            action?.Kill();
            
        }

    }
}