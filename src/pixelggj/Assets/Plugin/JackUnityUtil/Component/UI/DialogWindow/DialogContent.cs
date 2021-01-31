using System;
using System.Collections.Generic;

namespace JackUtil {

    [Serializable]
    public class DialogContent {

        public bool isPlayer { get; private set; }
        public string talker { get; set; }
        public string content { get; set; }

        public DialogContent(bool isPlayer, string talker, string content) {
            this.isPlayer = isPlayer;
            this.talker = talker;
            this.content = content;
        }

    }

    public static class DialogContentExtention {

        public static List<DialogContent> ReplacePlayerName(this List<DialogContent> list, string playerName) {
            foreach (var c in list) {
                if (c.isPlayer) {
                    c.talker = playerName;
                }
            }
            return list;
        }

        public static List<DialogContent> ReplaceNpcName(this List<DialogContent> list, string npcName) {
            foreach (var c in list) {
                if (!c.isPlayer) {
                    c.talker = npcName;
                }
            }
            return list;
        }

        public static List<DialogContent> ReplaceNames(this List<DialogContent> list, string playerName, string npcName) {
            foreach (var c in list) {
                if (c.isPlayer) {
                    c.talker = playerName;
                } else {
                    c.talker = npcName;
                }
            }
            return list;
        }
    }
}