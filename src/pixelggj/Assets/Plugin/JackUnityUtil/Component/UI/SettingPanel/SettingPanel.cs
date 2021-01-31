using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.EventSystems;
using DG.Tweening;
using Rewired;

namespace JackUtil {

    public class SettingPanel : PanelBase {

        SettingData settingData;

        public Canvas canvas;

        public Slider bgmSlider;
        Action<float> bgmChangeHandle;

        public Slider soundSlider;
        Action<float> soundChangeHandle;

        // 分辨率
        // 1280*720
        // 1600*900
        readonly Vector2Int baseResolution = new Vector2Int(320, 180);
        int baseMultiple = 4;
        public Dropdown resolutionDropdown;
        public Toggle fullScreenToggle;

        // 垂直同步
        [SerializeField] Toggle vSyneToggle;

        // 语言
        List<Toggle> langToggleList { get; } = new List<Toggle>();
        [SerializeField] Toggle cnToggle;
        [SerializeField] Toggle jpToggle;
        [SerializeField] Toggle enToggle;
        Action<LanguageType> langChangeHandle;
        LanguageType currentLang;

        // 显示时间
        public Toggle showTimeTextToggle;
        Action<bool> showTimeTextHandle;

        [SerializeField] Button yesButton;
        [SerializeField] Button noButton;

        // 缓存
        SettingData tempData;

        protected override GameObject defaultSelectedGo => bgmSlider.gameObject;

        protected override void Awake() {
            base.Awake();
            langToggleList.Add(cnToggle);
            langToggleList.Add(enToggle);
            langToggleList.Add(jpToggle);

            tempData = new SettingData();
        }

        protected override void Start() {

            base.Start();

            settingData = SettingData.LoadFromFile();

            if (settingData == null) {
                settingData = new SettingData();
                settingData.LoadDefault();
                settingData.SaveData();
            }

            LoadSetting(settingData);

            bgmSlider.onValueChanged.AddListener(volumn => {
                settingData.BGMVolumn = volumn;
                bgmChangeHandle?.Invoke(volumn);
            });

            soundSlider.onValueChanged.AddListener(volumn => {
                settingData.SoundVolumn = volumn;
                soundChangeHandle?.Invoke(volumn);
            });

            vSyneToggle.onValueChanged.AddListener(isVSync => {
                settingData.isVSync = isVSync;
                QualitySettings.vSyncCount = isVSync ? 1 : 0;
                Application.targetFrameRate = isVSync ? 120 : 100;
            });

            resolutionDropdown.onValueChanged.AddListener(optionIndex => {
                settingData.resolutionMultiple = resolutionDropdown.value + baseMultiple;
                SetResolution(GetResolution(optionIndex + baseMultiple), settingData.isFullScreen);
            });

            fullScreenToggle.onValueChanged.AddListener(isFull => {
                Screen.fullScreen = isFull;
                settingData.isFullScreen = isFull;
            });

            showTimeTextToggle.onValueChanged.AddListener(valueBool => {
                showTimeTextHandle?.Invoke(valueBool);
                settingData.isShowTimeText = valueBool;
            });

            cnToggle.onValueChanged.AddListener(valueBool => {
                if (currentLang == LanguageType.CN) {
                    cnToggle.isOn = true;
                    return;
                }
                if (valueBool) {
                    langChangeHandle?.Invoke(LanguageType.CN);
                    LoadLanguage(LanguageType.CN);
                }
            });

            enToggle.onValueChanged.AddListener(valueBool => {
                if (currentLang == LanguageType.EN) {
                    enToggle.isOn = true;
                    return;
                }
                if (valueBool) {
                    langChangeHandle?.Invoke(LanguageType.EN);
                    LoadLanguage(LanguageType.EN);
                }
            });

            jpToggle.onValueChanged.AddListener(valueBool => {
                if (currentLang == LanguageType.JP) {
                    jpToggle.isOn = true;
                    return;
                }
                if (valueBool) {
                    langChangeHandle?.Invoke(LanguageType.JP);
                    LoadLanguage(LanguageType.JP);
                }
            });

            yesButton.onClick.AddListener(() => {
                settingData.SaveData();
                Close();
            });

            noButton.onClick.AddListener(() => {
                LoadSetting(tempData);
                Close();
            });

            Close();

        }

        public override void Execute() {

            if (EventSystem.current.currentSelectedGameObject == null) {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(defaultSelectedGo);
            }

            if (uiInput == null) {
                DebugHelper.LogError("未执行Awake 或其他原因 导致uiInput为null");
                return;
            }

            if (uiInput.GetButtonUp("UIMenu")) {
                LoadSetting(tempData);
                Close();
            }

        }

        public void StorageTemp() {
            tempData = settingData.CreateDeepClone();
        }

        public void LoadLanguage(LanguageType lang) {
            currentLang = lang;
            Toggle witchIsOn = null;
            switch(lang) {
                case LanguageType.CN: witchIsOn = cnToggle; break;
                case LanguageType.EN: witchIsOn = enToggle; break;
                case LanguageType.JP: witchIsOn = jpToggle; break;
                default:
                    DebugHelper.LogError(lang.ToString() + "未本地化");
                    return;
            }
            foreach (var tg in langToggleList) {
                if (tg == witchIsOn) {
                    tg.isOn = true;
                } else {
                    tg.isOn = false;
                }
            }
        }

        public void RegisterBGMChange(Action<float> handle) {
            bgmChangeHandle = handle;
        }

        public void RegisterSoundChange(Action<float> handle) {
            soundChangeHandle = handle;
        }

        public void RegisterChangeLang(Action<LanguageType> handle) {
            langChangeHandle = handle;
        }

        public void RegisterChangeShowTimeText(Action<bool> handle) {
            showTimeTextHandle = handle;
        }

        void LoadSetting(SettingData data) {

            bgmChangeHandle?.Invoke(data.BGMVolumn);
            bgmSlider.value = data.BGMVolumn;

            soundChangeHandle?.Invoke(data.SoundVolumn);
            soundSlider.value = data.SoundVolumn;

            fullScreenToggle.isOn = data.isFullScreen;
            resolutionDropdown.value = data.resolutionMultiple - baseMultiple;
            SetResolution(GetResolution(data.resolutionMultiple), data.isFullScreen);

            vSyneToggle.isOn = data.isVSync;
            QualitySettings.vSyncCount = data.isVSync ? 1 : 0;
            Application.targetFrameRate = data.isVSync ? 120 : 100;

            showTimeTextToggle.isOn = data.isShowTimeText;
            showTimeTextHandle?.Invoke(data.isShowTimeText);

            settingData = data.CreateDeepClone();

        }

        Vector2Int GetResolution(int multiple) {
            if (multiple <= 3) {
                multiple = 3;
            } else if (multiple >= 6) {
                multiple = 6;
            }
            return baseResolution * multiple;
        }

        void SetResolution(Vector2Int resolution, bool isFullScreen) {
            CanvasScaler scaler = canvas.gameObject.GetComponent<CanvasScaler>();
            scaler.referenceResolution = new Vector2(resolution.x, resolution.y);
            Screen.SetResolution(resolution.x, resolution.y, isFullScreen);
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            langChangeHandle = null;
        }

        [Serializable]
        class SettingData {

            public float BGMVolumn { get; set; }
            public float SoundVolumn { get; set; }
            public int resolutionMultiple { get; set; }
            public bool isFullScreen { get; set; }
            public bool isShowTimeText { get; set; }
            public bool isVSync { get; set; }

            public SettingData() {
                this.resolutionMultiple = 4;
            }

            public void SaveData() {
                FileHelper.SaveFileBinary(this, Application.dataPath + "/settingdata");
            }

            public SettingData LoadDefault() {

                BGMVolumn = 1;
                SoundVolumn = 1;
                resolutionMultiple = 4;
                isFullScreen = true;
                isShowTimeText = false;
                isVSync = true;
                return this;

            }

            public SettingData LoadConfig() {
                SettingData data = FileHelper.LoadFileFromBinary<SettingData>(Application.dataPath + "/settingdata");
                BGMVolumn = data.BGMVolumn;
                SoundVolumn = data.SoundVolumn;
                resolutionMultiple = data.resolutionMultiple;
                isFullScreen = data.isFullScreen;
                isShowTimeText = data.isShowTimeText;
                isVSync = data.isVSync;
                return data;
            }

            public static SettingData LoadFromFile() {
                SettingData data = FileHelper.LoadFileFromBinary<SettingData>(Application.dataPath + "/settingdata");
                return data;
            }
        }
        
    }
}