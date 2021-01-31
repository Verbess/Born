using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Rewired;
using JackUtil;

namespace JackUtil {

    public class PausePanel : PanelBase {

        [SerializeField] Button continueButton;
        [SerializeField] Button rebornButton;
        [SerializeField] Button keyboardButton;
        [SerializeField] Button joystickButton;
        [SerializeField] Button optionButton;
        [SerializeField] Button toTitleButton;
        [SerializeField] Button exitGameButton;

        Action RebornHandle;
        Action OpenKeyboardHandle;
        Action OpenJoystickHandle;
        Action OpenSettingHandle;
        Action BackTitleHandle;
        Action ReturnGameHandle;

        protected override GameObject defaultSelectedGo => continueButton.gameObject;

        protected override void Start() {
            base.Start();

            ReInput.ControllerConnectedEvent += args => {
                if (args.controllerType == ControllerType.Joystick) {
                    joystickButton.Show();
                }
                if (args.controllerType == ControllerType.Keyboard) {
                    keyboardButton.Show();
                }
                ChooseDefaultGo();
            };

            ReInput.ControllerDisconnectedEvent += args => {
                if (args.controllerType == ControllerType.Joystick) {
                    joystickButton.Hide();
                }
                if (args.controllerType == ControllerType.Keyboard) {
                    keyboardButton.Hide();
                }
                ChooseDefaultGo();
            };

            if (ReInput.players.GetPlayer(0).controllers.Joysticks.Count > 0) {
                joystickButton.Show();
            } else {
                joystickButton.Hide();
            }

            if (ReInput.players.GetPlayer(0).controllers.hasKeyboard) {
                keyboardButton.Show();
            } else {
                keyboardButton.Hide();
            }

            continueButton.onClick.AddListener(() => {
                ReturnGameHandle();
            });

            rebornButton.onClick.AddListener(() => {
                RebornHandle();
            });

            keyboardButton.onClick.AddListener(() => {
                OpenKeyboardHandle();
            });

            joystickButton.onClick.AddListener(() => {
                OpenJoystickHandle();
            });

            optionButton.onClick.AddListener(()=> {
                OpenSettingHandle();
            });

            toTitleButton.onClick.AddListener(() => {
                ReturnGameHandle();
                BackTitleHandle();
            });

            exitGameButton.onClick.AddListener(Application.Quit);

            Close();

        }

        public void OnReborn(Action handle) => RebornHandle = handle;

        public void OnOpenKeyboard(Action handle) {
            OpenKeyboardHandle = handle;
        }

        public void OnOpenJoystick(Action handle) {
            OpenJoystickHandle = handle;
        }

        public void OnOpenSetting(Action handle) {
            OpenSettingHandle = handle;
        }

        public void OnBackTitle(Action handle) {
            BackTitleHandle = handle;
        }

        public void OnReturnGame(Action handle) {
            ReturnGameHandle = handle;
        }

        public override void Execute() {

            if (EventSystem.current.currentSelectedGameObject == null) {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(defaultSelectedGo);
            }

            if (uiInput == null) {
                return;
            }

            if (uiInput.GetButtonDown("UIMenu")) {
                ReturnGameHandle.Invoke();
            }

        }

        protected override void OnDestroy() {
            base.OnDestroy();
            ReturnGameHandle = null;
            BackTitleHandle = null;
            OpenSettingHandle = null;
            continueButton.onClick.RemoveAllListeners();
            optionButton.onClick.RemoveAllListeners();
            toTitleButton.onClick.RemoveAllListeners();
            exitGameButton.onClick.RemoveAllListeners();

        }

    }

}