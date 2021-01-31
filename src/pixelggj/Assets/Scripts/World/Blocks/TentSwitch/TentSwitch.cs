using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class TentSwitch : BlockBase {

        public override int id => BlockType.TentSwitch.ToInt();

        [SerializeField] Sprite opened;
        [SerializeField] Sprite closed;
        [SerializeField] Chain chain;

        NoticeWindow notice;

        public override void Render() {
            if (blockModel.isUsed) {
                sr.sprite = opened;
            } else {
                sr.sprite = closed;
            }
        }

        public override void Exchange(Actor actor) {
            base.Exchange(actor);
            BlockModel generalSwitch = data.gameData.GetBlockModel(BlockType.GeneralSwitch);
            if (generalSwitch.isUsed) {
                audioManager.PlayMapSound(MapSFX.Switcher);
                if (!blockModel.isUsed) {
                    chain.DropDown();
                } else {
                    chain.Raise();
                }
                blockModel.isUsed = !blockModel.isUsed;
                data.SaveData();
            } else {
                notice = (NoticeWindow)ui.OpenPanel(PanelType.Notice);
                notice.Init(data.localizationDao.GetNotice(NoticeType.NoPower));
            }
        }

    }
}