using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace JackUtil {

    public class WorldDialogWindow : DialogWindow, IWorldPanel {

        public Canvas worldCanvas;
        bool isSet = false;

        Vector3 playerPos;
        Vector3 npcPos;

        protected override void Start() {
            base.Start();
            Close();
        }

        public void Init(Vector3 npcPos, Vector3 playerPos, string notice = "") {
            this.npcPos = (Vector2)npcPos + Vector2.up * 2f + Vector2.right * 0.5f;
            this.playerPos = (Vector2)playerPos + Vector2.up * 2f;
            noticeText.text = notice;

            talkerText.color = JUIStyle.fontNormalColorDefalut;
            contentText.color = JUIStyle.fontNormalColorDefalut;
            noticeText.color = JUIStyle.fontNormalColorDefalut;
        }

        public virtual void SetCanvas(Canvas uiCanvas, Camera camera) {
            worldCanvas.WorldScaleToUICanvas(uiCanvas, camera);
            isSet = true;
        }

        public override void ShowCurrentDialog() {
            if (dialogIndex < dialogList.Count) {
                DialogContent c = dialogList[dialogIndex];
                talkerText.text = "" + c.talker + ": ";
                contentText.text = "  " + c.content + "    ";
                if (noticeText.text == "") {
                    noticeText.Hide();
                }
                if (c.talker == "") {
                    talkerText.Hide();
                }
                if (c.isPlayer) {
                    transform.position = playerPos;
                } else {
                    transform.position = npcPos;
                }
                Open();
            } else {
                Clear();
            }
        }

        void Update() {
            if (!isSet) {
                DebugHelper.LogError("需要初始化 -> 请调用 SetCanvas 方法");
                isSet = true;
            }
        }

    }
}