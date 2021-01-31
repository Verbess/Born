using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using JackUtil;

namespace PixelGGJNS {

    public class Axe : BlockBase {

        public override int id => BlockType.Axe.ToInt();

        [SerializeField] GameObject axe;

        public override BlockModel GenerateBlockModel() {
            blockModel = new BlockModel(id, false, false);
            return blockModel;
        }

        public override InventoryModel GatherInventory() {
            if (blockModel.isGathered) {
                return null;
            }
            return new InventoryModel(InventoryType.Axe);
        }

        public override void Exchange(Actor actor) {
            base.Exchange(actor);
            if (blockModel.isGathered == false) {
                ui.hudPage.inventoryGroup.InsertInventory(GatherInventory());
                blockModel.isGathered = true;
                audioManager.PlayMapSound(MapSFX.PickMatal);
                Render();
                data.SaveData();
            }
        }

        public override void Render() {
            if (blockModel.isGathered) {
                axe.Hide();
            } else {
                axe.Show();
            }
        }

    }

}