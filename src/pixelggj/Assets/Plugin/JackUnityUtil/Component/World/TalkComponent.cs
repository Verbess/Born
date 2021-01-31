using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JackUtil {

    [RequireComponent(typeof(Collider2D))]
    public class TalkComponent : MonoBehaviour {

        public List<DialogContent> contentList;

        protected virtual void Awake() {
            if (contentList == null) {
                contentList = new List<DialogContent>();
            }
        }

        public void Clear() {
            contentList?.Clear();
        }

        public void AddDialog(List<DialogContent> list) {
            contentList = list;
        }

        public void AddContent(DialogContent dialogContent) {
            contentList.Add(dialogContent);
        }

        public void AddContent(bool isPlayer, string talkerName, string content) {
            AddContent(new DialogContent(isPlayer, talkerName, content));
        }

    }

}