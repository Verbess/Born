using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using JackUtil;

namespace PixelGGJNS {

    public class ButtonClickSFX : MonoBehaviour, IPointerClickHandler {

        AudioManager audioManager => Container.Instance.audioManager;

        public void OnPointerClick(PointerEventData e) {
            if (e.button == PointerEventData.InputButton.Left) {
                audioManager.PlayUISound(UISFX.ButtonClick);
            }
        }
        
    }
}