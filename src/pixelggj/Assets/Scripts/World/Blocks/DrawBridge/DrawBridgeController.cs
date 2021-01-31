using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class DrawBridgeController : BlockBase {

        public override int id => BlockType.DrawBridgeController.ToInt();

        [SerializeField] Sprite jointedIn;
        [SerializeField] Sprite used;
        [SerializeField] DrawBridge bridge;

        [SerializeField] List<Sprite> animationList;
        int index;
        Sequence action;

        protected override void Awake() {
            base.Awake();
            index = 0;
        }

        public override void Render() {
            if (blockModel.isUsed) {
                BlockModel drawBridge = data.gameData.GetBlockModel(BlockType.DrawBridge);
                if (drawBridge.isUsed) {
                    sr.sprite = used;
                } else {
                    sr.sprite = jointedIn;
                }
            } else {
                sr.sprite = null;
            }
        }

        void Activated() {
            action?.Kill();
            action = DOTween.Sequence();
            action.AppendCallback(() => {
                index += 1;
                if (index >= animationList.Count) {
                    index = 0;
                }
                Sprite s = animationList[index];
                sr.sprite = s;
            });
            action.AppendInterval(0.06f);
            action.SetLoops(9);
        }

        public override void Exchange(Actor actor) {
            base.Exchange(actor);
            if (!blockModel.isUsed) {
                OneInventory cur = ui.hudPage.inventoryGroup.GetChosen();
                if (cur.model != null && cur.model.id == InventoryType.JointedArm.ToInt()) {
                    cur.UseInventory();
                    blockModel.isUsed = true;
                    Render();
                    data.SaveData();
                }
            } else {
                BlockModel drawBridge = data.gameData.GetBlockModel(BlockType.DrawBridge);
                if (!drawBridge.isUsed) {
                    ui.PlayFade();
                    audioManager.PlayMapSound(MapSFX.PutDownBridge);
                    action?.Kill();
                    action = DOTween.Sequence();
                    action.AppendInterval(1.5f);
                    action.AppendCallback(() => {
                        Activated();
                        bridge.Activated();
                    });
                }
            }
        }

    }
}