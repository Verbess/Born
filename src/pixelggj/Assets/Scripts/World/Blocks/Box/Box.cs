using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class Box : BlockBase {

        public override int id => BlockType.Box.ToInt();

        [SerializeField] Sprite locked;
        [SerializeField] Sprite unlocked;
        public GameObject box;
        public GameObject defaultPos;
        [SerializeField] Chain chain;

        public override void Render() {
            if (blockModel.isUsed) {
                sr.sprite = locked;
            } else {
                sr.sprite = unlocked;
            }
        }

        public override void Exchange(Actor actor) {
            base.Exchange(actor);
            if (!blockModel.isUsed) {
                BlockModel tentSwitchModel = data.gameData.GetBlockModel(BlockType.TentSwitch);
                if (tentSwitchModel.isUsed) {
                    chain.LockBox();
                    audioManager.PlayMapSound(MapSFX.PutItem);
                    sr.sprite = locked;
                    blockModel.isUsed = true;
                    data.SaveData();
                }
            }
        }

    }
}