using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class Anchor : BlockBase {

        public override int id => BlockType.Anchor.ToInt();

        [SerializeField] GameObject rope;
        [SerializeField] GameObject top;
        [SerializeField] GameObject bottom;
        [SerializeField] GameObject ground;
        [SerializeField] GameObject groundFinalPos;
        [SerializeField] GameObject finalPos;
        [SerializeField] GameObject checkPoint;
        [SerializeField] MapGo targetMap;
        [SerializeField] FloorArea bridgeControllerArea;
        Sequence action;

        bool isSliding;

        protected override void Awake() {
            base.Awake();
            isSliding = false;
        }

        public override void Render() {
            if (blockModel.isUsed) {
                rope.Show();
            } else {
                rope.Hide();
            }
        }

        void Update() {
            if (isSliding) {
                if (world.actor.transform.position.y < checkPoint.transform.position.y) {
                    world.EnterMap(targetMap);
                }
            }
        }

        public override BlockModel GenerateBlockModel() {
            return new BlockModel(id, false, false);
        }

        public override void Exchange(Actor actor) {
            base.Exchange(actor);
            if (!blockModel.isUsed) {
                OneInventory inventory = ui.hudPage.inventoryGroup.GetChosen();
                if (inventory.model != null && inventory.model.id == InventoryType.Rope.ToInt()) {
                    inventory.UseInventory();
                    audioManager.PlayMapSound(MapSFX.PutItem);
                    blockModel.isUsed = true;
                    Render();
                    data.SaveData();
                }
            } else {
                if (actor.transform.position.y < checkPoint.transform.position.y && bridgeControllerArea.isActorIn) {
                    MoveToGround(actor);
                } else {
                    MoveToBottom(actor);
                }
            }
        }

        void MoveToGround(Actor actor) {
            isSliding = true;
            action?.Kill();
            action = DOTween.Sequence();
            actor.LockAct(true);
            actor.PlayAni(AnimType.InRope);
            audioManager.PlayMapSound(MapSFX.SlideDownRope);
            actor.transform.position = bottom.transform.position;
            action.AppendInterval(0.5f);
            action.Append(actor.transform.DOMoveY(ground.transform.position.y, 2.5f));
            action.AppendCallback(() => {
                actor.PlayAni(AnimType.Idle);
                actor.PlayMoveAni(groundFinalPos.transform.position.x >= actor.transform.position.x);
            });
            action.Append(actor.transform.DOMove(groundFinalPos.transform.position, 0.5f));
            action.AppendCallback(() => {
                isSliding = false;
                actor.transform.position = groundFinalPos.transform.position;
                actor.SetTarget(groundFinalPos.transform.position);
                actor.LockAct(false);
            });
        }

        void MoveToBottom(Actor actor) {
            isSliding = true;
            action?.Kill();
            action = DOTween.Sequence();
            actor.LockAct(true);
            actor.PlayAni(AnimType.InRope);
            audioManager.PlayMapSound(MapSFX.SlideDownRope);
            actor.transform.position = top.transform.position;
            action.AppendInterval(0.5f);
            action.Append(actor.transform.DOMoveY(bottom.transform.position.y, 2.5f));
            action.AppendCallback(() => {
                actor.PlayAni(AnimType.Idle);
                actor.PlayMoveAni(finalPos.transform.position.x >= actor.transform.position.x);
            });
            action.Append(actor.transform.DOMove(finalPos.transform.position, 0.5f));
            action.AppendCallback(() => {
                isSliding = false;
                actor.transform.position = finalPos.transform.position;
                actor.SetTarget(finalPos.transform.position);
                actor.LockAct(false);
            });
        }

        void OnDestroy() {
            action?.Kill();
        }

    }
}