using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class Stair : BlockBase {

        public override int id => BlockType.Stair.ToInt();

        [SerializeField] GameObject centerPoint;
        [SerializeField] GameObject bottom;
        [SerializeField] GameObject bottomFinalPos;
        [SerializeField] GameObject top;
        [SerializeField] GameObject topFinalPos;

        Sequence action;

        public override void Render() {
            
        }

        public override void Exchange(Actor actor) {
            base.Exchange(actor);
            if (actor.transform.position.y > centerPoint.transform.position.y) {
                MoveDown(actor);
            } else {
                MoveUp(actor);
            }
        }

        void MoveDown(Actor actor) {
            actor.transform.position = topFinalPos.transform.position;
            MoveTo(actor, bottom, bottomFinalPos);
        }

        void MoveUp(Actor actor) {
            actor.transform.position = bottomFinalPos.transform.position;
            MoveTo(actor, top, topFinalPos);
        }

        void MoveTo(Actor actor, GameObject target1, GameObject target2) {
            action?.Kill();
            action = DOTween.Sequence();
            actor.LockAct(true);
            actor.PlayAni(AnimType.Climb);
            action.Append(actor.transform.DOMove(target1.transform.position, 3f));
            action.AppendCallback(() => {
                actor.PlayAni(AnimType.Idle);
                actor.PlayMoveAni(target2.transform.position.x >= actor.transform.position.x);
            });
            action.Append(actor.transform.DOMove(target2.transform.position, 0.5f));
            action.AppendCallback(() => {
                actor.SetTarget(target2.transform.position);
                actor.LockAct(false);
            });
        }

        void OnDestroy() {
            action?.Kill();
        }

    }

}