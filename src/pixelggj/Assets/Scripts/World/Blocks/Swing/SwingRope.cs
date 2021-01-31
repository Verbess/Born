using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class SwingRope : BlockBase {

        public override int id => BlockType.SwingRope.ToInt();

        [SerializeField] GameObject rope;
        [SerializeField] GameObject top;
        [SerializeField] GameObject middle;
        [SerializeField] GameObject bottom;
        [SerializeField] Swing swing;

        Sequence action;

        public override void Render() {
            MapGo map = transform.GetComponentInParent<MapGo>();
            LevelModel level = data.gameData.GetLevelModel(map.levelId);
            BlockModel pillarBlock = level.GetBlock(BlockType.RopePillar.ToInt());
            
            if (pillarBlock.isUsed) {
                rope.transform.position = middle.transform.position;
                if (blockModel.isUsed) {
                    rope.transform.position = bottom.transform.position;
                } else {
                    rope.transform.position = middle.transform.position;
                }
            } else {
                rope.transform.position = top.transform.position;
            }

        }

        public void Appear() {
            action?.Kill();
            action = DOTween.Sequence();
            action.Append(rope.transform.DOMove(middle.transform.position, 1.5f).SetEase(Ease.OutBounce));
            blockModel.isGathered = true;
            data.SaveData();
        }

        public override BlockModel GenerateBlockModel() {
            return new BlockModel(id, false, false);
        }

        public override void Exchange(Actor actor) {
            base.Exchange(actor);
            if (!blockModel.isGathered) {
                return;
            }
            if (!blockModel.isUsed) {
                audioManager.PlayMapSound(MapSFX.PutItem);
                swing.TryRaise();
            }
        }

        void OnDestroy() {
            action?.Kill();
        }
    }

}