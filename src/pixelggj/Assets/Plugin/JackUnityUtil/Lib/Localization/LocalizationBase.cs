using System;
using System.Collections.Generic;
using UnityEngine;

namespace JackUtil {

    public abstract class LocalizationBase : MonoBehaviour {

        public LanguageType lang { get; private set; }

        public void Init(LanguageType lang) {
            this.lang = LanguageType.None;
            ChangeLang(lang);
        }

        public virtual void ChangeLang(LanguageType lang) {
            this.lang = lang;
            switch(lang) {
                case LanguageType.CN: ToCN(); break;
                case LanguageType.EN: ToEN(); break;
                case LanguageType.JP: ToJP(); break;
                default:
                    DebugHelper.LogWarning(lang.ToString() + "未本地化, 使用默认中文");
                    ToCN();
                    break;
            }
        }
        protected virtual void ToCN() {
            throw new NotImplementedException();
        }
        protected virtual void ToEN() {
            DebugHelper.LogWarning(lang.ToString() + "未本地化, 使用默认中文");
            ToCN();
        }
        protected virtual void ToJP() {
            DebugHelper.LogWarning(lang.ToString() + "未本地化, 使用默认中文");
            ToCN();
        }

    }

}