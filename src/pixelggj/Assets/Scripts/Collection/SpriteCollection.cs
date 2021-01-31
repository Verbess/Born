using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JackUtil;

namespace PixelGGJNS {

    public class SpriteCollection : MonoBehaviour {

        Dictionary<int, Sprite> inventoryDic;
        [SerializeField] Sprite axe;
        [SerializeField] Sprite rope;
        [SerializeField] Sprite bridgeBoard;
        [SerializeField] Sprite jointedArm;
        [SerializeField] Sprite woodBoard;

        void Awake() {
            inventoryDic = new Dictionary<int, Sprite>();
            inventoryDic.AddOrReplace(InventoryType.Axe.ToInt(), axe);
            inventoryDic.AddOrReplace(InventoryType.Rope.ToInt(), rope);
            inventoryDic.AddOrReplace(InventoryType.SwingBoard.ToInt(), bridgeBoard);
            inventoryDic.AddOrReplace(InventoryType.JointedArm.ToInt(), jointedArm);
            inventoryDic.AddOrReplace(InventoryType.WoodBoard.ToInt(), woodBoard);
        }

        public Sprite GetInventory(int id) {
            Sprite s = inventoryDic.GetValue(id);
            if (s == null) {
                print(((InventoryType)id).ToString() + "不存在");
            }
            return s;
        }
    }
}