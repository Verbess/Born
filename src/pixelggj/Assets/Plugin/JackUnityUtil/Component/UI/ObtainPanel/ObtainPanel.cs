using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace JackUtil {

    public class ObtainPanel : PanelBase {

        public Button windowBtn;

        public Image itemImage;
        Sprite itemSprite;

        public Text contentText;

        public Text noticeText;

        protected override GameObject defaultSelectedGo => windowBtn.gameObject;

        protected override void Awake() {
            base.Awake();
            windowBtn.onClick.AddListener(Close);
        }

        protected override void Start() {
            Close();
        }

        public override void Execute() {
            base.Execute();
            if (uiInput.GetButtonUp("UISubmit")) {
                Close();
            }
        }

        public void ShowObtain(Sprite itemSprite, string content, Vector2 spriteSize = default, string notice = "") {
            this.itemSprite = itemSprite;
            itemImage.sprite = this.itemSprite;
            contentText.text = content;
            if (spriteSize != Vector2.zero) {
                itemImage.rectTransform.sizeDelta = spriteSize;
            }
            if (notice != "") {
                noticeText.text = notice;
                noticeText.Show();
            } else {
                noticeText.Hide();
            }
            Open();
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            windowBtn.onClick.RemoveAllListeners();
        }

    }

}