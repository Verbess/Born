using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class Stele : BlockBase {

        public override int id => BlockType.Stele.ToInt();

        DialogWindow dialog;
        [SerializeField] Transform dialogPos;

        public override void Render() {
            
        }

        public override void Exchange(Actor actor) {
            base.Exchange(actor);
            if (dialog == null) {
                dialog = (DialogWindow)ui.OpenPanel(PanelType.Dialog);
                dialog.Init();
                dialog.AddDialog(data.localizationDao.GetDialogContentList(3));
                dialog.ShowCurrentDialog();
                dialog.SetWorldPos(cameraManager.cam, dialogPos.transform.position);
                dialog.OnCloseOnce(() => {
                    dialog = null;
                });
            }
        }
    }
}