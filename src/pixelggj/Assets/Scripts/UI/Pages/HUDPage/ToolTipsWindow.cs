using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JackUtil;

namespace PixelGGJNS {

    public class ToolTipsWindow : MonoBehaviour {

        [SerializeField] GameObject bd;
        [SerializeField] Text content;
        [SerializeField] RectTransform rect;

        void Start() {
            Close();
        }

        public void Init(string content) {
            this.content.text = content;
        }

        public void SetPos(Vector2 localPos) {
            rect.localPosition = localPos;
        }

        public void SetWorldPos(Camera cam, Vector2 worldPos) {
            rect.localPosition = cam.WorldToScreenPoint(worldPos);
        }

        public void Open() {
            this.Show();
        }

        public void Close() {
            this.Hide();
        }

    }
}