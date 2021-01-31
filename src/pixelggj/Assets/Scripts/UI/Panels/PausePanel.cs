using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class PausePanel : PanelBase {

        protected override GameObject defaultSelectedGo => continueBtn.gameObject;

        WorldManager world => Container.Instance.world;
        UIManager ui => Container.Instance.ui;

        [SerializeField] Button continueBtn;
        [SerializeField] Button showKeysBtn;
        [SerializeField] Button backTitleBtn;
        [SerializeField] Button exitGameBtn;

        protected override void Start() {

            base.Start();

            continueBtn.onClick.AddListener(() => {
                Close();
                world.Pause(false);
            });

            showKeysBtn.onClick.AddListener(() => {
                ui.OpenPanel(PanelType.KeyBinding);
            });

            backTitleBtn.onClick.AddListener(() => {
                Close();
                ui.CloseCurrentPage();
                ui.EnterPage(PageType.Title);
            });

            exitGameBtn.onClick.AddListener(Application.Quit);

            Close();

        }

    }
}