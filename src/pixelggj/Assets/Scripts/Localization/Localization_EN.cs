using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JackUtil;

namespace PixelGGJNS {

    public partial class LocalizationDao : LocalizationBase {

        protected override void ToEN() {

            defaultFont = enFont;

            uiDic = new Dictionary<UIType, string>();
            uiDic.AddOrReplace(UIType.GameTitle, "Born");
            uiDic.AddOrReplace(UIType.ContinueGame, "Continue Game");
            uiDic.AddOrReplace(UIType.StartGame, "Start Game");
            uiDic.AddOrReplace(UIType.KeyList, "Keys List");
            uiDic.AddOrReplace(UIType.Pause, "Pause");
            uiDic.AddOrReplace(UIType.Setting, "Setting");
            uiDic.AddOrReplace(UIType.Continue, "Continue");
            uiDic.AddOrReplace(UIType.Yes, "Yes");
            uiDic.AddOrReplace(UIType.No, "No");
            uiDic.AddOrReplace(UIType.Confirm, "Confirm");
            uiDic.AddOrReplace(UIType.Cancel, "Cancel");
            uiDic.AddOrReplace(UIType.Close, "Close");
            uiDic.AddOrReplace(UIType.BackToTitle, "Back To Title");
            uiDic.AddOrReplace(UIType.ExitGame, "Exit Game");
            uiDic.AddOrReplace(UIType.ObtainPrefix, "Get ");
            uiDic.AddOrReplace(UIType.ObtainSuffix, ".");

            keyActionDic = new Dictionary<KeyActionType, string>();
            keyActionDic.AddOrReplace(KeyActionType.FourDirection, "Up/Down/Left/Right");
            keyActionDic.AddOrReplace(KeyActionType.Jump, "Jump");
            keyActionDic.AddOrReplace(KeyActionType.Skill, "Skill");
            keyActionDic.AddOrReplace(KeyActionType.Melee, "Melee");

            inventoryDic = new Dictionary<InventoryType, string>();
            inventoryDic.AddOrReplace(InventoryType.Axe, "Axe");
            inventoryDic.AddOrReplace(InventoryType.SwingBoard, "Swing Plate");
            inventoryDic.AddOrReplace(InventoryType.JointedArm, "Operating Lever");
            inventoryDic.AddOrReplace(InventoryType.WoodBoard, "Wooden Plate");
            inventoryDic.AddOrReplace(InventoryType.Rope, "Rope");

            noticeDic = new Dictionary<NoticeType, string>();
            noticeDic.AddOrReplace(NoticeType.DoorLocked, "That door seems to be locked from inside…");
            noticeDic.AddOrReplace(NoticeType.NoPower, "It looks like the power is off…");

            dialogDic = new Dictionary<int, List<DialogContent>>();
            string playerName = "Boy";
            string father = "Dad's Message";
            List<DialogContent> list;

            list = new List<DialogContent>();
            list.Add(new DialogContent(true, playerName, "Dad…"));
            list.Add(new DialogContent(true, playerName, "Go to sleep…"));
            list.Add(new DialogContent(true, playerName, "Now I have nobody…"));
            list.Add(new DialogContent(true, playerName, "Alone…"));
            dialogDic.Add(0, list);

            list = new List<DialogContent>();
            list.Add(new DialogContent(true, playerName, "！！！"));
            list.Add(new DialogContent(true, playerName, "那是！爸爸的遗物掉到山下去了！"));
            list.Add(new DialogContent(true, playerName, "……"));
            list.Add(new DialogContent(true, playerName, "我得去找回来…"));
            dialogDic.Add(1, list);

            list = new List<DialogContent>();
            list.Add(new DialogContent(true, playerName, "That drawbridge…"));
            list.Add(new DialogContent(true, playerName, "In my memories, I never cross that drawbridge…"));
            list.Add(new DialogContent(true, playerName, "It’s time to activate it."));
            dialogDic.Add(2, list);

            list = new List<DialogContent>();
            list.Add(new DialogContent(true, father, "Son:"));
            list.Add(new DialogContent(true, father, "At the time you find this stele,"));
            list.Add(new DialogContent(true, father, "it means you are a grownup now. "));
            list.Add(new DialogContent(true, father, "And now, you have to find your own road…"));
            list.Add(new DialogContent(true, father, "To downhill."));
            list.Add(new DialogContent(true, father, "Love, dad"));
            dialogDic.Add(3, list);
            
        }

    }

}