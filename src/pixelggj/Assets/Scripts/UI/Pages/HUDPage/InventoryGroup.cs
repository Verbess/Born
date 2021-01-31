using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JackUtil;

namespace PixelGGJNS {

    public class InventoryGroup : MonoBehaviour {

        DataManager data => Container.Instance.data;
        UIManager ui => Container.Instance.ui;
        SpriteCollection spriteCollection => Container.Instance.spriteCollection;

        [SerializeField] OneInventory inventoryPrefab;
        List<OneInventory> inventoryList;
        // [SerializeField] Image chosenIcon;
        int currentIndex;

        [SerializeField] RectTransform rect;
        [SerializeField] RectTransform left;
        [SerializeField] RectTransform right;
        sbyte isLeft;

        public GameObject tooltip;

        void Awake() {
            inventoryList = new List<OneInventory>();
            currentIndex = -1;
            isLeft = -1;
            tooltip?.Hide();
        }

        public void Render(InventoryModel[] inventories) {

            Clear();

            for (int i = 0; i < inventories.Length; i += 1) {
                InventoryModel model = inventories[i];
                OneInventory one = Instantiate(inventoryPrefab, transform);
                one.onChoose += ChooseInv;
                one.Render(model, i);
                inventoryList.Add(one);
            }

            if (currentIndex == -1) {
                Choose(0);
            }

        }

        public void SetLeft(bool isLeft) {
            sbyte tar = isLeft ? (sbyte)1 : (sbyte)0;
            if (this.isLeft == tar) {
                return;
            }
            this.isLeft = tar;
            rect.localPosition = isLeft ? left.localPosition : right.localPosition;
        }

        public void InsertInventory(InventoryModel model) {
            OneInventory one = inventoryList.Find(value => value.model == null);
            one.model = model;
            data.gameData.TryAddInventory(model);
            one.Render();
            ObtainPanel obtain = (ObtainPanel)ui.OpenPanel(PanelType.Obtain);
            Sprite icon = spriteCollection.GetInventory(model.id);
            obtain.ShowObtain(icon, data.localizationDao.GetUIName(UIType.ObtainPrefix) + data.localizationDao.GetInventory((InventoryType)model.id) + data.localizationDao.GetUIName(UIType.ObtainSuffix));
        }

        public OneInventory GetChosen() {
            return inventoryList[currentIndex];
        }

        void ChooseInv(OneInventory inventory) {
            // print(inventory.Index);
            Choose(inventory.Index);
        }

        void Choose(int index) {
            currentIndex = index;
            if (inventoryList == null || inventoryList.Count < index) {
                return;
            }

            foreach (var inv in inventoryList) {
                inv.Choose(false);
            }

            inventoryList[currentIndex].Choose(true);

            // float xMargin = -10f;
            // float yMargin = -10f;
            // float yGap = -8f;
            // float ySize = -60f;
            // chosenIcon.transform.localPosition = new Vector3(xMargin, yMargin + yGap * index + ySize * index);
            
        }

        void Clear() {
            for (int i = inventoryList.Count - 1; i >= 0; i -= 1) {
                Destroy(inventoryList[i].gameObject);
            }
            inventoryList.Clear();
        }

    }

}