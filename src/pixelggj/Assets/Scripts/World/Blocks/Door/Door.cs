using System;
using System.Collections.Generic;
using UnityEngine;
using JackUtil;

namespace PixelGGJNS {

    public class Door : BlockBase {

        public override int id => BlockType.Door.ToInt();

        public bool isActorStay;

        NoticeWindow notice;
        [SerializeField] Collider2D coll;
        [SerializeField] Sprite opened;

        protected override void Awake() {
            base.Awake();
            isActorStay = false;
        }

        public override BlockModel GenerateBlockModel() {
            blockModel = new BlockModel(id, true, false);
            return blockModel;
        }

        public override void Exchange(Actor actor) {
            base.Exchange(actor);
            if (isActorStay) {
                blockModel.isUsed = true;
                Render();
                audioManager.PlayMapSound(MapSFX.Unlock);
            } else {
                notice = (NoticeWindow)ui.OpenPanel(PanelType.Notice);
                notice.Init(data.localizationDao.GetNotice(NoticeType.DoorLocked));
                audioManager.PlayMapSound(MapSFX.Locked);
            }
        }

        public override void Render() {
            if (blockModel.isUsed) {
                sr.sprite = opened;
                coll.enabled = false;
            } else {
                sr.sprite = null;
                coll.enabled = true;
            }
        }
    }
}