using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JackUtil;

namespace PixelGGJNS {

    public class KeyActionLocalization : MonoBehaviour {

        [SerializeField] KeyActionType keyActionType;

        DataManager data => Container.Instance.data;

        Text text;

        void Awake() {
            text = GetComponent<Text>() ?? GetComponentInChildren<Text>();
            text.text = data.localizationDao.GetKeyActionName(keyActionType);
            LocalizationDao.ChangeLangHandle += lang => OnLangChange();
        }

        void OnLangChange() {
            text.text = data.localizationDao.GetKeyActionName(keyActionType);
            text.font = data.localizationDao.GetFont();
        }

    }
}