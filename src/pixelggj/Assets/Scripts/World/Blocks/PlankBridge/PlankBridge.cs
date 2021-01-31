using System;
using System.Collections.Generic;
using UnityEngine;
using JackUtil;

namespace PixelGGJNS {

    public class PlankBridge : BlockBase {

        public override int id => BlockType.PlankBridge.ToInt();

        [SerializeField] Collider2D coll;
        [SerializeField] Sprite board;

        public override BlockModel GenerateBlockModel() {
            blockModel = new BlockModel(id, true, false);
            return blockModel;
        }

        public override void Exchange(Actor actor) {
            base.Exchange(actor);
            OneInventory inventory = ui.hudPage.inventoryGroup.GetChosen();
            List<int> condition = new List<int>(){
                InventoryType.SwingBoard.ToInt(),
                InventoryType.WoodBoard.ToInt(),
            };
            if (inventory.model != null && condition.Contains(inventory.model.id)) {
                inventory.UseInventory();
                audioManager.PlayMapSound(MapSFX.PutItem);
                blockModel.isUsed = true;
                data.SaveData();
                Render();
            }
        }

        public override void Render() {
            if (blockModel.isUsed) {
                coll.enabled = false;
                sr.sprite = board;
            } else {
                coll.enabled = true;
                sr.sprite = null;
            }
        }

    }
}