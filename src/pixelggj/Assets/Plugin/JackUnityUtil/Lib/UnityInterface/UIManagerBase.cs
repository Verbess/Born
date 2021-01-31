using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JackUtil {

    public class UIManagerBase : MonoBehaviour {

        protected PanelBase currentPage { get; set; }

        protected Stack<PanelBase> panelStack { get; } = new Stack<PanelBase>();

        protected Dictionary<int, PanelBase> pageDic { get; } = new Dictionary<int, PanelBase>();
        protected Dictionary<int, PanelBase> panelDic { get; } = new Dictionary<int, PanelBase>();
        protected Dictionary<int, PanelBase> popWindowDic { get; } = new Dictionary<int, PanelBase>();

        protected virtual void Update() {

            if (panelStack.Count > 0) {
                panelStack.Peek().SetInteractable(true);
                panelStack.Peek().Execute();
                return;
            }

            currentPage.SetInteractable(true);
            currentPage.Execute();

        }

        public void RegisterPage(int pageType, PanelBase page) {
            pageDic.Add(pageType, page);
        }

        public void RegisterPanel(int panelType, PanelBase panel) {
            panelDic.Add(panelType, panel);
            panel.OnPop(PanelPop);
        }

        public void RegisterFreeWindow(int freeWindowType, PanelBase popWindow) {
            popWindowDic.Add(freeWindowType, popWindow);
        }

        public PanelBase EnterPage(int pageType) {
            PanelBase page = null;
            foreach (var kv in pageDic) {
                if (kv.Key == pageType) {
                    kv.Value.Open();
                    ChangePage(kv.Value);
                    page = kv.Value;
                }
            }
            return page;
        }

        public void ClosePage(int pageType) {
            pageDic.GetValue(pageType)?.Close();
        }

        public void CloseCurrentPage() {
            currentPage?.Close();
        }

        public PanelBase OpenPanel(int panelType) {
            PanelBase panel;
            panelDic.TryGetValue(panelType, out panel);
            panel.Open();
            foreach (PanelBase pb in panelStack) {
                pb.SetInteractable(false);
            }
            if (!panelStack.Contains(panel)) {
                // print("Push" + panel.name);
                panelStack.Push(panel);
            }
            panel.SetInteractable(true);
            currentPage.SetInteractable(false);
            return panel;
        }

        public void ClosePanel(int panelType) {
            PanelBase panel;
            panelDic.TryGetValue(panelType, out panel);
            panel.Close();
        }

        public PanelBase PopupFreeWindow(int freeWindowType) {
            PanelBase popwindow;
            popWindowDic.TryGetValue(freeWindowType, out popwindow);
            popwindow.Open();
            return popwindow;
        }

        void ChangePage(PanelBase page) {
            // print("ChangeToPage" + page.name);
            while(panelStack.Count > 0) {
                panelStack.Pop().Close();
            }
            currentPage = page;
            currentPage.SetInteractable(true);
        }

        void PanelPop() {
            if (panelStack.Count > 0) {
                PanelBase panel = panelStack.Pop();
                // print("Pop" + panel.name);
            }
        }

    }
}