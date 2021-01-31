using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rewired;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class TitlePage : PanelBase {

        protected override GameObject defaultSelectedGo {
            get {
                if (continueGameBtn.isActiveAndEnabled) {
                    return continueGameBtn.gameObject;
                } else {
                    return startGameBtn.gameObject;
                }
            }
        }

        WorldManager world => Container.Instance.world;
        UIManager ui => Container.Instance.ui;
        DataManager data => Container.Instance.data;

        [SerializeField] Button continueGameBtn;
        [SerializeField] Button startGameBtn;
        [SerializeField] Button keyListBtn;
        [SerializeField] Button exitGameBtn;
        [SerializeField] Button cnLangBtn;
        [SerializeField] Button enLangBtn;

        protected override void Start() {
            
            base.Start();

            CheckShowContinue();

            OnOpenOnce(CheckShowContinue);

            continueGameBtn.onClick.AddListener(() => {
                data.LoadGame();
                Close();
                ui.EnterPage(PageType.HUD);
                world.StartGame();
            });

            startGameBtn.onClick.AddListener(() => {
                data.NewGame();
                Close();
                ui.EnterPage(PageType.HUD);
                world.StartGame();
            });

            keyListBtn.onClick.AddListener(() => {
                ui.OpenPanel(PanelType.KeyBinding);
            });

            exitGameBtn.onClick.AddListener(Application.Quit);

            cnLangBtn.onClick.AddListener(() => {
                data.localizationDao.ChangeLang(LanguageType.CN);
                LocalizationDao.ChangeLangHandle?.Invoke(LanguageType.CN);
            });

            enLangBtn.onClick.AddListener(() => {
                data.localizationDao.ChangeLang(LanguageType.EN);
                LocalizationDao.ChangeLangHandle?.Invoke(LanguageType.EN);
            });

            Close();

        }

        void CheckShowContinue() {
            bool existsData = data.IsExistData();
            if (existsData) {
                continueGameBtn.Show();
            } else {
                continueGameBtn.Hide();
            }
        }

    }
}