using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class Swing : BlockBase {

        public override int id => BlockType.Swing.ToInt();

        [SerializeField] Sprite swingDown;
        [SerializeField] Sprite swingUp;
        [SerializeField] Sprite swingBroken;

        [SerializeField] SwingHeap swingHeap;
        [SerializeField] Transform seat;
        [SerializeField] GameObject swing;
        [SerializeField] GameObject top;
        [SerializeField] GameObject bottom;
        [SerializeField] GameObject checkPoint;
        [SerializeField] GameObject finalPos;
        [SerializeField] MapGo targetMap;
        Sequence action;

        public override BlockModel GenerateBlockModel() {
            blockModel = new BlockModel(id, false, false);
            return blockModel;
        }

        public override void Render() {
            if (blockModel.isUsed) {
                sr.sprite = swingUp;
                if (blockModel.isGathered) {
                    sr.sprite = swingBroken;
                }
            } else {
                sr.sprite = swingDown;
            }
        }

        // void Update() {
        //     if (blockModel != null && !blockModel.isUsed) {
        //         if (checkPoint.activeSelf && swing.transform.position.y > checkPoint.transform.position.y) {
        //             world.EnterMap(targetMap);
        //             checkPoint.Hide();
        //         }
        //     }
        // }

        public void TryRaise() {

            if (!isActorColl || blockModel.isUsed) {
                return;
            }

            Actor actor = world.actor;

            MoveToSeat(actor, () => {

                // 如果人站在上面，升起
                // 并执行拉绳动作
                // RaiseWithAni(actor);
                RaiseWithSprite(actor);
                
            });

        }

        void RaiseWithSprite(Actor actor) {
            actor.LockAct(true);
            action?.Kill();
            action = DOTween.Sequence();
            ui.PlayFade();
            action.AppendInterval(1f);
            action.AppendCallback(() => {
                blockModel.isUsed = true;
                world.EnterMap(targetMap);
                Render();
                actor.transform.position = finalPos.transform.position;
                actor.SetTarget(finalPos.transform.position);
                actor.LockAct(false);
            });
        }

        void RaiseWithAni(Actor actor) {
            actor.LockAct(true);

            action?.Kill();
            action = DOTween.Sequence();
            actor.transform.parent = swing.transform;
            action.Append(swing.transform.DOMove(top.transform.position, 5f));
            action.AppendCallback(() => {
                actor.transform.parent = null;
            });
            action.Append(actor.transform.DOMove(finalPos.transform.position, 0.5f));
            action.AppendCallback(() => {
                actor.SetTarget(finalPos.transform.position);
                actor.LockAct(false);
                blockModel.isUsed = true;
            });
        }

        public override void Exchange(Actor actor) {
            
            base.Exchange(actor);

            if (!blockModel.isUsed) {
                MoveToSeat(actor);
            } else {
                if (!blockModel.isGathered) {
                    OneInventory cur = ui.hudPage.inventoryGroup.GetChosen();
                    if (cur.model != null && cur.model.id == InventoryType.Axe.ToInt()) {
                        swing.Hide();
                        blockModel.isGathered = true;
                        swingHeap.Activated();
                        data.SaveData();
                    }
                }
            }

        }

        void MoveToSeat(Actor actor, Action cb = null) {

            actor.LockAct(true);

            action?.Kill();
            action = DOTween.Sequence();
            action.Append(actor.transform.DOMove(seat.transform.position, 0.5f));
            action.AppendCallback(() => {
                actor.transform.position = seat.transform.position;
                actor.SetTarget(seat.transform.position);
                actor.LockAct(false);
                cb?.Invoke();
            });

        }

        void OnDestroy() {
            action?.Kill();
        }

    }

}