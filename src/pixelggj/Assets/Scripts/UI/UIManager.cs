using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rewired;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public enum PageType : byte {
        Title,
        HUD,
    }

    public enum PanelType : byte {
        KeyBinding,
        Pause,
        Dialog,
        GameOver,
        Obtain,
        Notice,
        Curtain,
    }

    public class UIManager : UIManagerBase {

        WorldManager world => Container.Instance.world;

        public TitlePage titlePage;
        public HUDPage hudPage;

        public PausePanel pausePanel;
        public GameOverPanel gameOverPanel;
        public ObtainPanel obtainPanel;

        public DialogWindow dialogWindow;
        public KeyBindingWindow keyBindingWindow;
        public NoticeWindow noticeWindow;
        public CurtainWindow curtainWindow;

        void Awake() {

            // 注册页面
            RegisterPage((int)PageType.Title, titlePage);
            RegisterPage((int)PageType.HUD, hudPage);

            // 注册面板
            RegisterPanel((int)PanelType.Pause, pausePanel);
            RegisterPanel((int)PanelType.KeyBinding, keyBindingWindow);
            RegisterPanel((int)PanelType.Dialog, dialogWindow);
            RegisterPanel((int)PanelType.GameOver, gameOverPanel);
            RegisterPanel((int)PanelType.Obtain, obtainPanel);
            RegisterPanel((int)PanelType.Notice, noticeWindow);
            RegisterPanel((int)PanelType.Curtain, curtainWindow);

            pausePanel.OnCloseOnce(() => {
                world.Pause(false);
            });

            gameOverPanel.OnCloseOnce(() => {
                CloseCurrentPage();
                EnterPage(PageType.Title);
            });

        }

        void Start() {

            EnterPage((int)PageType.Title);

        }

        public PanelBase EnterPage(PageType pageType) {
            return EnterPage((int)pageType);
        }

        public PanelBase OpenPanel(PanelType panel) {
            return OpenPanel((int)panel);
        }

        public void PlayFade() {
            OpenPanel(PanelType.Curtain);
            curtainWindow.PlayFade(0.8f, 0.8f, 0.8f);
        }

    }

}