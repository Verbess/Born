using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JackUtil;

namespace PixelGGJNS {

    public class TextLocalization : MonoBehaviour {

        [SerializeField] UIType uiType;

        DataManager data => Container.Instance.data;

        Text text;

        void Awake() {
            text = GetComponent<Text>() ?? GetComponentInChildren<Text>();
            text.text = data.localizationDao.GetUIName(uiType);
            LocalizationDao.ChangeLangHandle += lang => OnLangChange();
        }

        void OnLangChange() {
            text.text = data.localizationDao.GetUIName(uiType);
            text.font = data.localizationDao.GetFont();
        }

    }
}