using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class SwingHeap : BlockBase {

        public override int id => BlockType.SwingHeap.ToInt();

        [SerializeField] Collider2D coll;

        public override void Render() {
            BlockModel swing = data.gameData.GetBlockModel(BlockType.Swing);
            if (!swing.isGathered) {
                this.Hide();
            } else {
                this.Show();
                coll.enabled = true;
            }
        }

        public void Activated() {
            this.Show();
            coll.enabled = true;
            audioManager.PlayMapSound(MapSFX.WoodOnGrass);
        }

        public override void Exchange(Actor actor) {
            base.Exchange(actor);
            BlockModel swing = data.gameData.GetBlockModel(BlockType.Swing);
            if (swing.isGathered) {
                if (!blockModel.isGathered) {
                    InventoryModel rope = new InventoryModel(InventoryType.Rope);
                    ui.hudPage.inventoryGroup.InsertInventory(rope);
                    ui.obtainPanel.OnCloseOnce(() => {
                        InventoryModel bridgeBoard = new InventoryModel(InventoryType.SwingBoard);
                        ui.hudPage.inventoryGroup.InsertInventory(bridgeBoard);
                        blockModel.isGathered = true;
                        audioManager.PlayMapSound(MapSFX.UseAxe);
                        this.Hide();
                        data.SaveData();
                        ui.obtainPanel.OnCloseOnce(null);
                    });
                }
            }
        }


    }
}