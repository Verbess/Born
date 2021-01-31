using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rewired;

namespace JackUtil {

    public abstract class PanelDataBase {
    
    }

    [RequireComponent(typeof(CanvasGroup))]
    public abstract class PanelBase : MonoBehaviour {

        Dictionary<string, Action> eventDic { get; } = new Dictionary<string, Action>();
        protected abstract GameObject defaultSelectedGo { get; }

        [HideInInspector] public CanvasGroup canvasGroup;

        Action PopHandle;
        Action closeHandle;
        Action openHandle;
        Action destroyHandle;

        protected Player uiInput => ReInput.players.GetSystemPlayer();

        protected virtual void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.interactable = false;
        }

        protected virtual void Start() {
            
        }

        public virtual void SetInteractable(bool isInteractable) {
            canvasGroup.interactable = isInteractable;
        }

        public virtual void Init<T>(T panelData) where T : PanelDataBase {
            Debug.Log("未重写");
        }

        public virtual void Execute() {

            if (EventSystem.current.currentSelectedGameObject == null) {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(defaultSelectedGo);
            }

            if (uiInput == null) {
                return;
            }

            if (uiInput.GetButtonUp("UIMenu")) {
                Close();
            }

        }

        public virtual void RegisterEvent(string eventName, Action handle) {
            eventDic.AddOrReplace(eventName, handle);
        }

        public virtual void On(string eventName, Action handle) {
            RegisterEvent(eventName, handle);
        }

        public virtual void OnCloseOnce(Action handle) {
            closeHandle = handle;
        }

        public virtual void OnOpenOnce(Action handle) {
            openHandle = handle;
        }

        public virtual void OnPop(Action handle) {
            PopHandle = handle;
        }

        public virtual void OnDestroyOnce(Action handle) {
            destroyHandle = handle;
        }

        public virtual void TriggerEvent(string eventName) {
            if (eventDic.ContainsKey(eventName)) {
                Action a = eventDic[eventName];
                a.Invoke();
            }
        }

        public virtual void RemoveEvent(string eventName) {
            if (eventDic.ContainsKey(eventName)) {
                eventDic.Remove(eventName);
            }
        }

        public virtual void Open() {
            gameObject.SetActive(true);
            gameObject.transform.SetAsLastSibling();
            ChooseDefaultGo();
            openHandle?.Invoke();
            // print("打开" + this);
            // openHandle = null;
        }

        public virtual void ChooseDefaultGo() {
            if (!isActiveAndEnabled) return;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(defaultSelectedGo);
        }

        [ContextMenu("Close")]
        public virtual void Close() {
            EventSystem.current.SetSelectedGameObject(null);
            PopHandle?.Invoke();
            gameObject.SetActive(false);
            closeHandle?.Invoke();
            // print("关闭" + this);
            // closeHandle = null;
        }

        // public virtual void DestroyThis() {
        //     PopHandle?.Invoke();
        //     Destroy(gameObject);
        //     if (destroyHandle == null) {
        //         closeHandle?.Invoke();
        //         closeHandle = null;
        //     } else {
        //         destroyHandle?.Invoke();
        //         destroyHandle = null;
        //     }
        // }

        public void SetWorldPos(Camera camera, Vector3 worldPos) {
            Vector3 pos = camera.WorldToScreenPoint(worldPos);
            transform.position = pos;
        }

        protected virtual void OnDestroy() {
            PopHandle = null;
            eventDic.Clear();
            destroyHandle?.Invoke();
            destroyHandle = null;
            openHandle = null;
            closeHandle = null;
        }

    }
}