using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class Cave : BlockBase {

        public override int id => BlockType.Cave.ToInt();

        [SerializeField] Collider2D coll;
        [SerializeField] GameObject top;
        [SerializeField] GameObject topFinal;
        [SerializeField] GameObject center;
        [SerializeField] GameObject bottom;
        [SerializeField] GameObject bottomFinal;

        Sequence action;

        public override void Render() {
            
        }

        public override void Exchange(Actor actor) {
            base.Exchange(actor);
            if (actor.transform.position.y > center.transform.position.y) {
                MoveDown(actor);
            } else {
                MoveUp(actor);
            }
        }

        public void ShowCave(bool isShow) {
            coll.enabled = isShow;
        }

        void MoveDown(Actor actor) {
            action?.Kill();
            action = DOTween.Sequence();
            ui.PlayFade();
            actor.LockAct(true);
            action.Append(actor.transform.DOMove(top.transform.position, 0.8f));
            action.AppendInterval(0.8f);
            action.AppendCallback(() => {
                actor.transform.position = bottom.transform.position;
                actor.SetTarget(bottom.transform.position);
                actor.LockAct(false);
            });
        }

        void MoveUp(Actor actor) {
            action?.Kill();
            action = DOTween.Sequence();
            ui.PlayFade();
            actor.LockAct(true);
            action.Append(actor.transform.DOMove(bottom.transform.position, 0.8f));
            action.AppendInterval(0.8f);
            action.AppendCallback(() => {
                actor.transform.position = top.transform.position;
                actor.SetTarget(top.transform.position);
                actor.LockAct(false);
            });
        }

        void MoveMultiple(Actor actor, GameObject target1, GameObject target2) {
            action?.Kill();
            action = DOTween.Sequence();
            ui.PlayFade();
            actor.LockAct(true);
            action.Append(actor.transform.DOMove(target1.transform.position, 4f));
            action.Append(actor.transform.DOMove(target2.transform.position, 0.5f));
            action.AppendCallback(() => {
                actor.SetTarget(target2.transform.position);
                actor.LockAct(false);
            });
        }

    }
}