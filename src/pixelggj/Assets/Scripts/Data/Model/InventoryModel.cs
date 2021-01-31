using System;
using System.Collections.Generic;

namespace PixelGGJNS {

    public enum InventoryType {
        Axe,
        SwingBoard,
        Rope,
        JointedArm,
        WoodBoard,
    }

    public static class InventoryTypeExtention {
        public static int ToInt(this InventoryType inventory) {
            return (int)inventory;
        }
    }

    [Serializable]
    public class InventoryModel {

        public int id { get; set; }

        public InventoryModel(InventoryType inventory) {
            this.id = (int)inventory;
        }

    }
}