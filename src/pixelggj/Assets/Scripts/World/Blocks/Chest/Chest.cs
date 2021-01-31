using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class Chest : BlockBase {

        public override int id => BlockType.Chest.ToInt();

        [SerializeField] Sprite opened;
        [SerializeField] Sprite closed;

        public override void Render() {
            if (blockModel.isGathered) {
                sr.sprite = opened;
            } else {
                sr.sprite = closed;
            }
        }

        public override BlockModel GenerateBlockModel() {
            blockModel = new BlockModel(id, false, false);
            return blockModel;
        }

        public override void Exchange(Actor actor) {
            base.Exchange(actor);
            if (!blockModel.isGathered) {
                InventoryModel inventory = new InventoryModel(InventoryType.JointedArm);
                ui.hudPage.inventoryGroup.InsertInventory(inventory);
                audioManager.PlayMapSound(MapSFX.PickWood);
                blockModel.isGathered = true;
                data.SaveData();
                Render();
            }
        }
    }
}