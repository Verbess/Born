using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JackUtil;

namespace PixelGGJNS {

    public partial class LocalizationDao : LocalizationBase {

        protected override void ToCN() {

            defaultFont = cnFont;

            uiDic = new Dictionary<UIType, string>();
            uiDic.AddOrReplace(UIType.GameTitle, "伯恩");
            uiDic.AddOrReplace(UIType.ContinueGame, "继续游戏");
            uiDic.AddOrReplace(UIType.StartGame, "开始游戏");
            uiDic.AddOrReplace(UIType.KeyList, "按键表");
            uiDic.AddOrReplace(UIType.Pause, "暂停");
            uiDic.AddOrReplace(UIType.Setting, "设置");
            uiDic.AddOrReplace(UIType.Continue, "继续");
            uiDic.AddOrReplace(UIType.Yes, "是");
            uiDic.AddOrReplace(UIType.No, "否");
            uiDic.AddOrReplace(UIType.Confirm, "确认");
            uiDic.AddOrReplace(UIType.Cancel, "取消");
            uiDic.AddOrReplace(UIType.Close, "关闭");
            uiDic.AddOrReplace(UIType.BackToTitle, "返回标题");
            uiDic.AddOrReplace(UIType.ExitGame, "退出游戏");
            uiDic.AddOrReplace(UIType.ObtainPrefix, "获得了 ");
            uiDic.AddOrReplace(UIType.ObtainSuffix, "。");

            keyActionDic = new Dictionary<KeyActionType, string>();
            keyActionDic.AddOrReplace(KeyActionType.FourDirection, "上/下/左/右");
            keyActionDic.AddOrReplace(KeyActionType.Jump, "跳");
            keyActionDic.AddOrReplace(KeyActionType.Skill, "技能");
            keyActionDic.AddOrReplace(KeyActionType.Melee, "攻击");

            inventoryDic = new Dictionary<InventoryType, string>();
            inventoryDic.AddOrReplace(InventoryType.Axe, "斧子");
            inventoryDic.AddOrReplace(InventoryType.SwingBoard, "秋千板");
            inventoryDic.AddOrReplace(InventoryType.JointedArm, "操纵杆");
            inventoryDic.AddOrReplace(InventoryType.WoodBoard, "木板");
            inventoryDic.AddOrReplace(InventoryType.Rope, "绳索");

            noticeDic = new Dictionary<NoticeType, string>();
            noticeDic.AddOrReplace(NoticeType.DoorLocked, "门好像从里面反锁了。");
            noticeDic.AddOrReplace(NoticeType.NoPower, "似乎没有通电。");

            dialogDic = new Dictionary<int, List<DialogContent>>();
            string playerName = "男孩";
            string father = "父亲留下的话";
            List<DialogContent> list;

            list = new List<DialogContent>();
            list.Add(new DialogContent(true, playerName, "爸爸…"));
            list.Add(new DialogContent(true, playerName, "您安息吧…"));
            list.Add(new DialogContent(true, playerName, "只剩我一个人…"));
            list.Add(new DialogContent(true, playerName, "好孤独…"));
            dialogDic.Add(0, list);

            list = new List<DialogContent>();
            list.Add(new DialogContent(true, playerName, "！！！"));
            list.Add(new DialogContent(true, playerName, "那是！爸爸的遗物掉到山下去了！"));
            list.Add(new DialogContent(true, playerName, "……"));
            list.Add(new DialogContent(true, playerName, "我得去找回来…"));
            dialogDic.Add(1, list);

            list = new List<DialogContent>();
            list.Add(new DialogContent(true, playerName, "那个吊桥…"));
            list.Add(new DialogContent(true, playerName, "自我记事起，从没有跨过这座桥…"));
            list.Add(new DialogContent(true, playerName, "去找找过桥的方法吧…"));
            dialogDic.Add(2, list);

            list = new List<DialogContent>();
            list.Add(new DialogContent(true, father, "儿子："));
            list.Add(new DialogContent(true, father, "当了你跨过了这座桥"));
            list.Add(new DialogContent(true, father, "意味着你已经长大了"));
            list.Add(new DialogContent(true, father, "之后下山的路"));
            list.Add(new DialogContent(true, father, "要靠你自己的力量去走了。"));
            list.Add(new DialogContent(true, father, "——爱你的爸爸"));
            dialogDic.Add(3, list);

        }

    }

}