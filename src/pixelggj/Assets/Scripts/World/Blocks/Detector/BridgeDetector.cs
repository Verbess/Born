using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class BridgeDetector : MonoBehaviour {

        CameraManager cameraManager => Container.Instance.cameraManager;
        DataManager data => Container.Instance.data;
        WorldManager world => Container.Instance.world;
        UIManager ui => Container.Instance.ui;

        [SerializeField] Transform pos;
        Sequence action;

        DialogWindow dialog;

        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == TagCollection.PLAYER) {
                if (!data.gameData.IsCompleteSceneEvent(SceneEvent.MeetDrawBridge)) {
                    Actor actor = world.actor;
                    actor.LockAct(true);
                    action?.Kill();
                    action = DOTween.Sequence();
                    action.AppendInterval(1f);
                    action.AppendCallback(() => {
                        actor.PlayAni(AnimType.Idle);
                        actor.PlayMoveAni(pos.position.x >= actor.transform.position.x);
                    });
                    action.Append(actor.transform.DOMove(pos.position, 2f));
                    action.AppendCallback(() => {
                        actor.PlayAni(AnimType.Idle);
                        if (dialog == null) {
                            dialog = (DialogWindow)ui.OpenPanel(PanelType.Dialog);
                            dialog.Init();
                            dialog.AddDialog(data.localizationDao.GetDialogContentList(2));
                            dialog.SetWorldPos(cameraManager.cam, actor.transform.position + new Vector3(-8, 0, 0));
                            dialog.ShowCurrentDialog();
                            dialog.OnCloseOnce(() => {
                                dialog = null;
                                data.gameData.CompleteSceneEvent(SceneEvent.MeetDrawBridge);
                                actor.LockAct(false);
                                data.SaveData();
                            });
                        }
                    });

                }
            }
        }

        void OnDestroy() {
            action?.Kill();
        }

    }
}