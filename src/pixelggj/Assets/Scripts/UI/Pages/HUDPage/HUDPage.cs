using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rewired;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class HUDPage : PanelBase {

        protected override GameObject defaultSelectedGo => null;

        DataManager data => Container.Instance.data;
        CameraManager cameraManager => Container.Instance.cameraManager;
        WorldManager world => Container.Instance.world;
        UIManager ui => Container.Instance.ui;

        public InventoryGroup inventoryGroup;

        bool isEnable;

        protected override void Awake() {

            base.Awake();
            isEnable = false;

            OnOpenOnce(() => {
                inventoryGroup.Render(data.gameData.inventories);
            });

            OnCloseOnce(() => {
                isEnable = false;
            });

        }

        public override void Execute() {

            if (EventSystem.current.currentSelectedGameObject == null) {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(defaultSelectedGo);
            }

            if (uiInput == null) {
                return;
            }

            isEnable = true;

            world.Execute();

            if (uiInput.GetButtonUp("UIMenu")) {
                world.Pause(true);
                ui.OpenPanel(PanelType.Pause);
            }

        }

        public void FixedUpdate() {

            if (isEnable) {

                world.FixedExecute();

            }

        }

    }

}