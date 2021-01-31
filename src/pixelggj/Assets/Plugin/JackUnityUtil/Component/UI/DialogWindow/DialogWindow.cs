using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace JackUtil {

    public class DialogWindow : PanelBase {

        protected List<DialogContent> dialogList { get; } = new List<DialogContent>();
        protected int dialogIndex { get; set; } = -1;
        [SerializeField] float maxWidth = 300;
        [SerializeField] RectTransform border;
        [SerializeField] HorizontalLayoutGroup layoutGroup;

        [SerializeField] protected Button windowButton;
        [SerializeField] protected Text talkerText;
        [SerializeField] protected Text contentText;
        [SerializeField] protected Text noticeText;
        string prefix = "";

        [NonSerialized]
        public DialogContent currentDialog;

        protected override GameObject defaultSelectedGo => windowButton.gameObject;

        protected override void Awake() {
            base.Awake();
        }

        protected override void Start() {
            windowButton.onClick.AddListener(NextDialog);
            noticeText.Hide();
            Close();
        }

        public override void Execute() {

            if (EventSystem.current.currentSelectedGameObject == null) {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(defaultSelectedGo);
            }

            if (uiInput == null) {
                return;
            }

            if (uiInput.GetButtonUp("UIMenu")) {
                Clear();
            }

            if (uiInput.GetButtonUp("UISubmit")) {
                NextDialog();
            }

        }

        public virtual void Init(string notice = "") {

            noticeText.text = notice;
            dialogIndex = 0;

            talkerText.color = JUIStyle.fontNormalColorDefalut;
            contentText.color = JUIStyle.fontNormalColorDefalut;
            noticeText.color = JUIStyle.fontNormalColorDefalut;

        }

        public bool IsCompleted() {
            return dialogIndex == -1 || dialogIndex >= dialogList.Count || dialogList.Count == 0;
        }

        public void AddDialog(bool isPlayer, string talkerName, string content) {
            dialogList.Add(new DialogContent(isPlayer, talkerName, content));
        }

        public void AddContent(DialogContent content) {
            dialogList.Add(content);
        }

        public void AddDialog(List<DialogContent> list) {
            dialogList.AddRange(list);
        }

        public void AddDialog(List<DialogContent> list, string replacePlayerName) {
            foreach (DialogContent c in list) {
                if (c.isPlayer) {
                    c.talker = replacePlayerName;
                }
                dialogList.Add(c);
            }
        }

        public void AddDialog(List<DialogContent> list, string replacePlayerName, string replaceNpcName) {
            foreach (DialogContent c in list) {
                if (c.isPlayer) {
                    c.talker = replacePlayerName;
                } else {
                    c.talker = replaceNpcName;
                }
                dialogList.Add(c);
            }
        }

        public void SetNotice(string content) {
            noticeText.text = content;
            noticeText.Show();
        }

        public void Clear() {
            dialogList.Clear();
            dialogIndex = -1;
            Close();
        }

        public virtual void ShowCurrentDialog() {

            if (dialogIndex < dialogList.Count) {

                if (IsTyping()) {
                    contentText.ShowFullContent();
                    return;
                }
                
                currentDialog = dialogList[dialogIndex];
                string contentStr = prefix + currentDialog.content;
                JudgeContentSize(contentStr);

                talkerText.text = currentDialog.talker + ": \r\n";
                contentText.TypingFX(contentStr, 0.048f);
                if (noticeText.text == "") {
                    noticeText.Hide();
                }
                if (currentDialog.talker == "") {
                    talkerText.Hide();
                }

                Open();

            } else {

                Clear();

            }
        }

        void JudgeContentSize(string targetContext) {
            if ((targetContext.Length + 4) * contentText.fontSize > maxWidth) {
                layoutGroup.childControlWidth = false;
                border.sizeDelta = new Vector2(maxWidth, border.sizeDelta.y);
            } else {
                layoutGroup.childControlWidth = true;
            }
        }

        bool IsTyping() {
            if (currentDialog == null) {
                return false;
            }
            string contentStr = prefix + currentDialog.content;
            if (contentText.text == contentStr) {
                return false;
            }
            return true;
        }

        public void NextDialog() {
            if (!IsTyping()) {
                dialogIndex += 1;
            }
            ShowCurrentDialog();
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            windowButton.onClick.RemoveAllListeners();
        }

    }
}