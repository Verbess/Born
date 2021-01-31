using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class RIP : BlockBase {

        public override int id => BlockType.RIP.ToInt();

        List<int> dialogIndexList;
        int index;

        DialogWindow dialog;

        [SerializeField] Transform startPos;
        [SerializeField] Transform cliff;
        Sequence action;

        protected override void Awake() {
            dialogIndexList = new List<int>() {
                0
            };
            index = 0;
        }

        public override BlockModel GenerateBlockModel() {
            blockModel = new BlockModel(id, true, true);
            return blockModel;
        }

        public override void Render() {
            
        }

        public void ActivatedEvent() {

            Actor actor = world.actor;
            actor.LockAct(true);
            action?.Kill();
            action = DOTween.Sequence();
            action.AppendInterval(1f);
            action.AppendCallback(() => {
                actor.PlayAni(AnimType.Idle);
                // actor.PlayMoveAni(true);
                actor.sr.flipX = true;
            });
            // action.Append(actor.transform.DOMove(startPos.transform.position, 2f));
            action.AppendCallback(() => {
                // 执行事件
                if (dialog == null) {
                    List<DialogContent> list = data.localizationDao.GetDialogContentList(0);
                    dialog = (DialogWindow)ui.OpenPanel(PanelType.Dialog);
                    dialog.Init();
                    dialog.AddDialog(list);
                    dialog.ShowCurrentDialog();
                    dialog.SetWorldPos(cameraManager.cam, actor.transform.position + new Vector3(0, 5f, 0));
                    dialog.OnCloseOnce(() => {
                        dialog = null;
                        actor.LockAct(false);
                        data.gameData.CompleteSceneEvent(SceneEvent.TalkRIP);
                        data.SaveData();
                    });
                }
            });

        }

        public override void Exchange(Actor actor) {

            base.Exchange(actor);

            if (dialog == null) {
                List<DialogContent> list = data.localizationDao.GetDialogContentList(0);
                dialog = (DialogWindow)ui.OpenPanel(PanelType.Dialog);
                dialog.Init();
                dialog.AddDialog(list);
                dialog.ShowCurrentDialog();
                dialog.SetWorldPos(cameraManager.cam, actor.transform.position + new Vector3(0, 5f, 0));
                dialog.OnCloseOnce(() => {
                    dialog = null;
                });
            }
            
        }
    }
}