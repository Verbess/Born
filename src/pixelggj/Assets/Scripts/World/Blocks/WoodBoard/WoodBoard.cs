using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class WoodBoard : BlockBase {

        public override int id => BlockType.WoodBoard.ToInt();

        [SerializeField] Collider2D coll;
        [SerializeField] Sprite wood;

        public override void Render() {
            if (blockModel.isGathered) {
                sr.sprite = null;
                coll.enabled = false;
            } else {
                sr.sprite = wood;
                coll.enabled = true;
            }
        }

        public override void Exchange(Actor actor) {
            base.Exchange(actor);
            if (!blockModel.isGathered) {
                InventoryModel model = new InventoryModel(InventoryType.WoodBoard);
                audioManager.PlayMapSound(MapSFX.PickWood);
                ui.hudPage.inventoryGroup.InsertInventory(model);
                blockModel.isGathered = true;
                Render();
                data.SaveData();
            }
        }
    }
}