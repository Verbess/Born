using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

namespace JackUtil {

    public class OneKeyBinding : MonoBehaviour {

        public KeyCode keyCode { get; private set; }
        public ControllerType controllerType { get; private set; }
        public InputMapper.Context context { get; private set; }
        public ControllerMap map { get; private set; }
        public string actionDescriptiveName { get; private set; }

        public Text keyNameText;
        public Button button;
        public Text buttonNameText;

        ActionElementMap elementMap;

        public Action<OneKeyBinding> HandleListening;

        void Awake() {

            button.onClick.AddListener(() => {
                buttonNameText.text = "(按下新按键)";
                HandleListening(this);
            });

        }

        public void SetNavagation(Selectable up, Selectable down, Selectable left, Selectable right) {
            Navigation nav = new Navigation();
            nav.mode = Navigation.Mode.Explicit;
            nav.selectOnUp = up;
            nav.selectOnDown = down;
            nav.selectOnLeft = left;
            nav.selectOnRight = right;
            button.navigation = nav;
        }

        public void RenderKey(int playerId, InputAction inputAction, AxisRange axisRange, ControllerType controllerType, ControllerMap map, ActionElementMap ele) {
            this.controllerType = controllerType;
            context = new InputMapper.Context() {
                actionId = inputAction.id,
                actionRange = axisRange,
                controllerMap = map,
                actionElementMapToReplace = ele
            };
            this.map = map;
            elementMap = ele;
            RenderKey();
        }

        public void RenderKey() {
            if (elementMap != null) {
                // keyNameText.text = elementMap.actionDescriptiveName;
                actionDescriptiveName = elementMap.actionDescriptiveName;
                if (controllerType == ControllerType.Joystick) {
                    buttonNameText.text = elementMap.elementIdentifierName;
                } else {
                    keyCode = elementMap.keyCode;
                    buttonNameText.text = keyCode.ToNonePrefixString();
                }
            }
        }

    }
}