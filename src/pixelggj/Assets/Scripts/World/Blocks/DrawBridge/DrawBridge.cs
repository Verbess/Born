using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class DrawBridge : BlockBase {

        public override int id => BlockType.DrawBridge.ToInt();

        [SerializeField] Sprite bridgeDown;
        [SerializeField] Sprite bridgeNormal;
        [SerializeField] Collider2D coll;
        [SerializeField] SpriteRenderer mask;

        public override void Render() {
            if (blockModel.isUsed) {
                sr.sprite = bridgeDown;
                mask.enabled = true;
                coll.enabled = false;
            } else {
                sr.sprite = bridgeNormal;
                mask.enabled = false;
                coll.enabled = true;
            }
        }

        public void Activated() {
            if (!blockModel.isUsed) {
                blockModel.isUsed = true;
                Render();
            }
        }
    }
}