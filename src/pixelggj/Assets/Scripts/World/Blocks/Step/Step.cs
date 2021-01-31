using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class Step : BlockBase {

        public override int id => BlockType.Step.ToInt();

        [SerializeField] Sprite board;
        [SerializeField] Collider2D coll;

        public override void Render() {
            if (blockModel.isUsed) {
                sr.sprite = board;
                coll.enabled = false;
            } else {
                sr.sprite = null;
                coll.enabled = true;
            }
        }

        public override void Exchange(Actor actor) {
            base.Exchange(actor);
            if (!blockModel.isUsed) {
                OneInventory cur = ui.hudPage.inventoryGroup.GetChosen();
                List<int> condition = new List<int>(){
                    InventoryType.SwingBoard.ToInt(),
                    InventoryType.WoodBoard.ToInt(),
                };
                if (cur.model != null && condition.Contains(cur.model.id)) {
                    audioManager.PlayMapSound(MapSFX.PutItem);
                    blockModel.isUsed = true;
                    cur.UseInventory();
                    Render();
                    data.SaveData();
                }
            }
        }
    }
}