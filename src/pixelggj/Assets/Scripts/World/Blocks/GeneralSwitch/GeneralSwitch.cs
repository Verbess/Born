using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class GeneralSwitch : BlockBase {

        public override int id => BlockType.GeneralSwitch.ToInt();

        [SerializeField] SpriteRenderer house;
        [SerializeField] Sprite houseBlack;
        [SerializeField] Sprite houseBright;

        public override BlockModel GenerateBlockModel() {
            blockModel = new BlockModel(id, true, false);
            return blockModel;
        }

        public override void Exchange(Actor actor) {

            base.Exchange(actor);

            audioManager.PlayMapSound(MapSFX.Switcher);

            blockModel.isUsed = !blockModel.isUsed;

            data.SaveData();

            Render();

        }

        public override void Render() {
            if (blockModel.isUsed) {
                house.sprite = houseBright;
            } else {
                house.sprite = houseBlack;
            }
        }
    }
}