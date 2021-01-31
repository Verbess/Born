using System;
using System.Collections.Generic;
using UnityEngine;
using JackUtil;

namespace PixelGGJNS {

    public class GameOverArea : MonoBehaviour {

        DataManager data => Container.Instance.data;
        UIManager ui => Container.Instance.ui;

        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == TagCollection.PLAYER) {
                ui.OpenPanel(PanelType.GameOver);
            }
        }

    }
}