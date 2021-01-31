using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JackUtil;

namespace PixelGGJNS {

    public enum UIType {
        GameTitle,
        StartGame,
        Setting,
        KeyList,
        Pause,
        Continue,
        Yes,
        No,
        Confirm,
        Cancel,
        Close,
        BackToTitle,
        ExitGame,
        ContinueGame,
        ObtainPrefix,
        ObtainSuffix,
    }

    public enum KeyActionType {
        FourDirection,
        Jump,
        Skill,
        Melee,
    }

    public enum NoticeType {
        DoorLocked,
        NoPower,
    }

    [AddComponentMenu("Localization/LocalizationDao")]
    public partial class LocalizationDao : LocalizationBase {

        public static Action<LanguageType> ChangeLangHandle;

        Font defaultFont;
        [SerializeField] Font enFont;
        [SerializeField] Font cnFont;

        Dictionary<UIType, string> uiDic;
        Dictionary<KeyActionType, string> keyActionDic;
        Dictionary<InventoryType, string> inventoryDic;
        Dictionary<int, List<DialogContent>> dialogDic;
        Dictionary<NoticeType, string> noticeDic;

        public Font GetFont() => defaultFont;
        public string GetUIName(UIType ui) => uiDic.GetValue(ui);
        public string GetKeyActionName(KeyActionType ui) => keyActionDic.GetValue(ui);
        public string GetInventory(InventoryType inventory) => inventoryDic.GetValue(inventory);
        public string GetNotice(NoticeType notice) => noticeDic.GetValue(notice);
        public List<DialogContent> GetDialogContentList(int index) => dialogDic.GetValue(index);

    }

}