using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace JackUtil {

    public abstract class StaffRollPanelBase : PanelBase {

        public abstract GameObject rollBD { get; }
        public Vector2 finalPos = new Vector2(0, 0);

        Vector2 defaultPos;

        public Image bgImg;

        public float defaultSpeed = 1f;
        float speed;
        Sequence rollAction;
        Sequence fadeInAction;
        Sequence fadeOutAction;

        protected override void Awake() {
            base.Awake();
            speed = defaultSpeed;
            bgImg.DOFade(0, 0);
            defaultPos = rollBD.transform.localPosition;
        }

        public void Reset() {
            rollBD.transform.localPosition = defaultPos;
        }

        public void Begin() {

            FadeIn();

            rollAction?.Kill();
            rollAction = DOTween.Sequence();
            rollAction.AppendCallback(() => {
                Vector2 pos = rollBD.transform.localPosition;
                rollBD.transform.localPosition = new Vector2(pos.x, pos.y + speed);
                if (rollBD.transform.localPosition.y > finalPos.y) {
                    rollAction?.Kill();
                }
            });
            rollAction.AppendInterval(0.016f);
            rollAction.SetLoops(-1);
            rollAction.onKill = FadeOut;
        }

        public override void Close() {
            base.Close();
            rollAction?.Kill();
            fadeInAction?.Kill();
            fadeOutAction?.Kill();
        }

        void FadeOut() {
            fadeOutAction?.Kill();
            fadeOutAction = DOTween.Sequence();
            fadeOutAction.AppendInterval(5);
            fadeOutAction.AppendCallback(() => {
                Close();
            });
        }

        void FadeIn() {
            fadeInAction?.Kill();
            fadeInAction = DOTween.Sequence();
            fadeInAction.Append(bgImg.DOFade(0.9f, 1.5f));
        }

        protected void SetSpeed(float speed) {
            this.speed = speed;
        }

        protected void UseDefaultSpeed() {
            speed = defaultSpeed;
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            rollAction?.Kill();
            fadeInAction?.Kill();
            fadeOutAction?.Kill();
        }
    }

}