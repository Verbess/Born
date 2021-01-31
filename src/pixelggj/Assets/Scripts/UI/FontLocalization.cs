using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JackUtil;

namespace PixelGGJNS {

    public class FontLocalization : MonoBehaviour {

        DataManager data => Container.Instance.data;

        Text text;

        void Awake() {
            text = GetComponent<Text>() ?? GetComponentInChildren<Text>();
            LocalizationDao.ChangeLangHandle += lang => OnLangChange();
        }

        void OnLangChange() {
            text.font = data.localizationDao.GetFont();
        }

    }
}