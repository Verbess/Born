using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace JackUtil {

    public static class TextExtention {

        static Dictionary<Text, Sequence> typingFXDic { get; } = new Dictionary<Text, Sequence>();

        public static void TypingFX(this Text t, string content, float gapTime) {
            Sequence action;
            if (typingFXDic.ContainsKey(t)) {
                action = typingFXDic[t];
                action?.Kill();
                action = DOTween.Sequence();
            } else {
                action = DOTween.Sequence();
                typingFXDic.Add(t, action);
            }
            int index = 0;
            t.text = "";
            action.AppendInterval(gapTime);
            action.AppendCallback(() => {
                t.text += content[index];
                index += 1;
            });
            action.SetLoops(content.Length);
            action.onKill = () => t.text = content;
        }

        public static void ShowFullContent(this Text t) {
            Sequence action = typingFXDic.GetValue(t);
            action?.Kill();
            typingFXDic.TryRemove(t);
        }

    }
}