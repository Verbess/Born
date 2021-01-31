using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using JackUtil;

namespace PixelGGJNS {

    [RequireComponent(typeof(Button))]
    public class OneInventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        SpriteCollection spriteCollection => Container.Instance.spriteCollection;
        DataManager data => Container.Instance.data;
        UIManager ui => Container.Instance.ui;
        CameraManager cameraManager => Container.Instance.cameraManager;

        public InventoryModel model;

        [SerializeField] Button button;
        [SerializeField] Image inventoryIcon;
        [SerializeField] RawImage bgIcon;
        [SerializeField] Sprite normalIcon;
        [SerializeField] Sprite chosenIcon;

        int index;
        public int Index => index;
        public Action<OneInventory> onChoose;

        GameObject tooltip;
        Text tooltipText;

        void Start() {
            button.onClick.AddListener(() => {
                onChoose.Invoke(this);
            });
        }

        public void OnPointerEnter(PointerEventData e) {
            if (model == null) {
                return;
            }
            tooltip = tooltip ?? ui.hudPage.inventoryGroup.tooltip;
            tooltip.Show();
            tooltipText = tooltipText ?? tooltip.GetComponentInChildren<Text>();
            tooltipText.text = data.localizationDao.GetInventory((InventoryType)model.id);
        }

        public void OnPointerExit(PointerEventData e) {
            tooltip?.Hide();
        }

        public void Render(InventoryModel model, int index) {
            this.model = model;
            this.index = index;
            if (model == null) {
                inventoryIcon.Hide();
            } else {
                inventoryIcon.Show();
                inventoryIcon.sprite = spriteCollection.GetInventory(model.id);
            }
        }

        public void UseInventory() {
            if (model != null) {
                data.gameData.UseInventory(model);
                this.model = null;
                inventoryIcon.Hide();
            }
        }

        public void Choose(bool isChoose) {
            bgIcon.texture = isChoose ? chosenIcon.texture : normalIcon.texture;
        }

        public void Render() {
            if (model == null) {
                inventoryIcon.Hide();
            } else {
                inventoryIcon.Show();
                inventoryIcon.sprite = spriteCollection.GetInventory(model.id);
            }
        }

    }
}