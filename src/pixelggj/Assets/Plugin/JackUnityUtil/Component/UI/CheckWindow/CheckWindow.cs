using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JackUtil {

    public class CheckWindow : PanelBase {

        public GameObject titleBD;
        public Text titleText;
        public Text contentText;
        public Button yesButton;
        public Button noButton;

        Action yesHandle;
        Action noHandle;

        protected override GameObject defaultSelectedGo => yesButton.gameObject;

        protected override void Awake() {
            base.Awake();
            yesButton.onClick.AddListener(() => {
                yesHandle?.Invoke();
                Close();
            });
            noButton.onClick.AddListener(() => {
                noHandle?.Invoke();
                Close();
            });
        }

        protected override void Start() {
            base.Start();
            Close();
        }

        public void Init(string title, string content) {
            if (title == "") {
                titleBD.Hide();
            } else {
                titleBD.Show();
                titleText.text = title;
            }
            contentText.text = content;
        }

        public void OnYesOnce(string buttonName, Action action) {
            if (buttonName != "") {
                yesButton.SetText(buttonName);
            }
            yesHandle = action;
        }

        public void OnNoOnce(string buttonName, Action action) {
            if (buttonName != "") {
                noButton.SetText(buttonName);
            }
            noHandle = action;
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
            yesHandle = null;
            noHandle = null;
        }

    }
}