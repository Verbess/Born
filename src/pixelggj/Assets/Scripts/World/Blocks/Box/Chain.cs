using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class Chain : BlockBase {

        public override int id => BlockType.Chain.ToInt();

        [SerializeField] Sprite chainSprite;
        [SerializeField] Box box;
        [SerializeField] Cave cave;
        [SerializeField] GameObject chain;
        [SerializeField] GameObject middle;
        [SerializeField] GameObject bottom;
        [SerializeField] MapGo currentMap;
        [SerializeField] MapGo targetMap;

        Sequence action;

        protected override void Awake() {
            base.Awake();
        }

        public override void Render() {
            BlockModel model = data.gameData.GetBlockModel(BlockType.TentSwitch);
            cave.ShowCave(false);
            if (model.isUsed) {
                chain.transform.position = bottom.transform.position;
            } else {
                BlockModel boxModel = data.gameData.GetBlockModel(BlockType.Box);
                chain.transform.position = bottom.transform.position;
                sr.sprite = chainSprite;
                if (boxModel.isUsed) {
                    box.box.transform.parent = chain.transform;
                    sr.sprite = null;
                    cave.ShowCave(true);
                } else {
                    box.box.transform.parent = box.transform;
                    box.box.transform.position = box.defaultPos.transform.position;
                }
                chain.transform.position = middle.transform.position;
            }
        }

        public void DropDown() {
            MoveTo(world.actor, bottom);
        }

        public void Raise() {
            MoveTo(world.actor, middle);
        }

        public void LockBox() {
            box.box.transform.parent = chain.transform;
            sr.sprite = null;
        }

        void MoveTo(Actor actor, GameObject target) {
            BlockModel boxModel = data.gameData.GetBlockModel(BlockType.Box);
            if (boxModel.isUsed) {
                box.box.transform.parent = chain.transform;
                sr.sprite = null;
            } else {
                sr.sprite = chainSprite;
            }
            cameraManager.SetFollowingLimited(chain.gameObject, targetMap.transform.position, targetMap.size);
            actor.LockAct(true);
            action?.Kill();
            action = DOTween.Sequence();
            action.AppendInterval(1f);
            action.AppendCallback(() => {
                audioManager.PlayMapSound(MapSFX.Machinary);
            });
            action.Append(chain.transform.DOMoveY(target.transform.position.y, 2.5f));
            action.AppendCallback(() => {
                if (boxModel.isUsed && target == middle) {
                    cave.ShowCave(true);
                } else {
                    cave.ShowCave(false);
                }
                actor.SetTarget(actor.transform.position);
                actor.LockAct(false);
                world.EnterMap(currentMap);
            });
        }

        void OnDestroy() {
            action?.Kill();
        }

    }
}