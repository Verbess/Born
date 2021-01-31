using System;
using System.Collections.Generic;
using UnityEngine;
using JackUtil;

namespace PixelGGJNS {

    public class RopePillar : BlockBase {

        public override int id => BlockType.RopePillar.ToInt();

        [SerializeField] SwingRope swingRope;
        [SerializeField] Sprite bindingRope;
        [SerializeField] Collider2D coll;

        public override void Render() {
            if (blockModel != null && !blockModel.isUsed) {
                sr.sprite = bindingRope;
                coll.enabled = true;
            } else {
                sr.sprite = null;
                coll.enabled = false;
            }
        }

        public override BlockModel GenerateBlockModel() {
            blockModel = new BlockModel(id, false, false);
            return blockModel;
        }

        public override void Exchange(Actor actor) {
            base.Exchange(actor);
            if (blockModel != null && !blockModel.isUsed) {
                swingRope.Appear();
                blockModel.isUsed = true;
                audioManager.PlayMapSound(MapSFX.PutItem);
                data.SaveData();
                Render();
            }
        }

    }
}