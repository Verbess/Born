using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rewired;
using Rewired.Data;

namespace JackUtil {

    public class KeyBindingPanel : PanelBase {

        Player playerInput;

        int playerId = 0;

        InputMapper mapper;

        [SerializeField] ControllerType controllerType;

        [SerializeField] GameObject rootBd;

        [SerializeField] GameObject keysBdPrefab;
        [SerializeField] OneKeyBinding keyBindingPrefab;

        string deadZonePath = "JackUtilJoystickDeadZone";
        [NonSerialized]
        public float deadZoneValue = 0;
        Func<string> deadZontTitleLocalizationFunc;
        [SerializeField] GameObject deadZoneBD;
        [SerializeField] Text deadZoneText;

        public Slider deadZoneSlider;
        [SerializeField] Button restoreDefaultButton;
        [SerializeField] Button yesButton;
        [SerializeField] Button noButton;
        Action yesHandle;

        List<GameObject> keysBdList { get; } = new List<GameObject>();
        // Dictionary<string, OneKeyBinding> keyBindingDic { get; } = new Dictionary<string, OneKeyBinding>();
        List<OneKeyBinding> keyBindingList { get; } = new List<OneKeyBinding>();

        OneKeyBinding handleKeybinding;

        protected override GameObject defaultSelectedGo {
            get {
                if (keyBindingList.Count > 0) {
                    return keyBindingList.First()?.button.gameObject;
                }
                return null;
            }
        }
        InputMapper.ConflictFoundEventData conflictFoundEventData;

        protected override void Awake() {

            base.Awake();

            ReInput.userDataStore.Load();
            playerInput = ReInput.players.GetPlayer(0);

            restoreDefaultButton.onClick.AddListener(() => {

                PlayerPrefs.DeleteAll();
                ReInput.userDataStore.Load();

                GameObject inputManager = GameObject.Find("Rewired Input Manager");
                inputManager.SetActive(false);
                inputManager.SetActive(true);
                
                LoadButtonList();
                // Close();
            });

            yesButton.onClick.AddListener(() => {
                SaveChange();
                Close();
                yesHandle?.Invoke();
            });

            noButton.onClick.AddListener(() => {
                ReInput.userDataStore.Load();
                Close();
            });

            // ---- Step 1 ----
            // InputMapper 是核心，用于监听改键事件
            mapper = new InputMapper();
            mapper.ConflictFoundEvent += data => {

                conflictFoundEventData = data;
                bool isConflict = false;
                
                ControllerMap map = handleKeybinding.map;

                // 外置键盘 / 笔记本内置键盘冲突
                if (map.controllerType == ControllerType.Keyboard || map.controllerType == ControllerType.Custom) {

                    foreach (ActionElementMap action in map.AllMaps) {
                        if (action.keyCode == data.assignment.keyCode) {
                            // print("键盘内部发生冲突，不变");
                            isConflict = true;
                            break;
                        }
                    }

                // 手柄冲突
                } else if (map.controllerType == ControllerType.Joystick) {

                    foreach (ActionElementMap action in map.AllMaps) {
                        if (action.controllerMap.categoryId == data.assignment.action.categoryId) {
                            if (action.elementIdentifierId == data.assignment.elementIdentifier.id) {
                                // print("手柄内部发生冲突，不变");
                                isConflict = true;
                                break;
                            }
                        }
                    }

                // 笔记本键盘冲突
                } else {

                }

                if (!isConflict) {
                    // print("外部发生冲突，添加");
                    data.responseCallback(InputMapper.ConflictResponse.Add);
                }
                handleKeybinding = null;
                
                LoadButtonList();
            };
            mapper.InputMappedEvent += inputEventData => { // 改键完成时，触发 InputMappedEvent
                LoadButtonList();
            };
            mapper.TimedOutEvent += timoutData => {
                handleKeybinding.RenderKey();
            };
            mapper.StoppedEvent += stopEventData => {
                handleKeybinding = null;
                uiInput.controllers.maps.SetMapsEnabled(true, "UI");
            };
            mapper.options.timeout = 10f; // 设置监听总时长，超过之后就会触发 TimeoutEvent
            mapper.options.ignoreMouseXAxis = true;
            mapper.options.ignoreMouseYAxis = true;
        }

        protected override void Start() {

            deadZoneSlider.onValueChanged.AddListener(value => {
                deadZoneText.text = deadZontTitleLocalizationFunc?.Invoke() + ": " + string.Format("{0:F1}", value) + "";
            });

            LoadButtonList();
            LoadDeadZone();

            Close();

        }

        public void LoadDeadZone() {

            if (controllerType != ControllerType.Joystick) {
                deadZoneBD.Hide();
            } else {
                deadZoneBD.Show();
            }

            deadZoneValue = GetDeadZone();

            if (deadZoneValue == 0) {
                deadZoneSlider.value = 39;
                SaveDeadZone(deadZoneSlider.value);
            }

            deadZoneSlider.value = deadZoneValue;
            deadZoneText.text = deadZontTitleLocalizationFunc?.Invoke() + ": " + string.Format("{0:F1}", deadZoneValue) + "°";

        }

        public float GetDeadZone() {
            return FileHelper.LoadFileFromBinary<float>(Application.dataPath + "/" + deadZonePath);
        }

        void SaveDeadZone(float deadZoneValue) {
            FileHelper.SaveFileBinary(deadZoneValue, Application.dataPath + "/" + deadZonePath);
        }

        public override void Execute() {

            if (EventSystem.current.currentSelectedGameObject == null) {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(defaultSelectedGo);
            }

            if (uiInput.GetButtonDown("UIMenu")) {
                if (handleKeybinding == null) {
                    Close();
                }
            }

        }

        public override void Open() {
            base.Open();
            LoadButtonList();
        }

        public override void Close() {
            base.Close();
            mapper.Stop();
        }

        public void OnYes(Action yesHandle) {
            this.yesHandle = yesHandle;
        }

        public void OnDeadZoneLocalization(Func<string> deadZoneLocalizationFunc) {
            this.deadZontTitleLocalizationFunc = deadZoneLocalizationFunc;
        }

        void HandleListening(OneKeyBinding keyBinding) {

            handleKeybinding = keyBinding;

            // ---- Step 2 ----
            // 一定不能让 Mapper 的监听阻塞主线程
            // 所以这里用协程的方式启动监听
            StartCoroutine(StartListening(keyBinding));

        }

        IEnumerator StartListening(OneKeyBinding keyBinding) {
            yield return new WaitForSeconds(0.16f);
            // ---- Step 3 ----
            // 启动监听，5秒内可以设置按键
            mapper.Start(keyBinding.context);
            // 这里一定要把UI InputModule暂时禁用
            uiInput.controllers.maps.SetMapsEnabled(false, "UI");
        }

        void SaveChange() {

            PlayerPrefs.DeleteAll();
            ReInput.userDataStore.Save();

            deadZoneValue = deadZoneSlider.value;
            SaveDeadZone(deadZoneSlider.value);

        }

        void LoadButtonList() {

            Clear();
            playerInput = ReInput.players.GetPlayer(0);

            // (如果需要)加载手柄 DeadZone
            LoadDeadZone();

            // 加载World
            var maps = playerInput.controllers.maps.GetAllMaps();
            foreach (var map in maps) {
                if (map.controllerType != controllerType) continue;
                if (map.categoryId == 1) {
                    ShowKeybindingTables(map.categoryId, map);
                }
            }

            // 加载UI
            maps = uiInput.controllers.maps.GetAllMaps();
            foreach (var map in maps) {
                if (map.controllerType != controllerType) continue;
                if (map.categoryId == 2) {
                    ShowKeybindingTables(map.categoryId, map);
                }
            }

            JudgeNavagation();

            handleKeybinding = null;

        }

        void ShowKeybindingTables(int selectedCategoryId, ControllerMap selectedMap) {

            InputMapCategory mapCategory = ReInput.mapping.GetMapCategory(selectedCategoryId);
            if (mapCategory == null) {
                DebugHelper.Log("不存在");
                return;
            }

            InputCategory actionCategory = ReInput.mapping.GetActionCategory(mapCategory.name);
            if (actionCategory == null) {
                DebugHelper.Log("不存在");
                return;
            }

            GameObject bd = Instantiate(keysBdPrefab, rootBd.transform);
            int siblingIndex = 0;
            if (controllerType == ControllerType.Joystick) {
                siblingIndex = 3;
            } else if (controllerType == ControllerType.Keyboard) {
                siblingIndex = 2;
            }
            bd.transform.SetSiblingIndex(bd.transform.parent.childCount - siblingIndex);
            keysBdList.Add(bd);

            foreach (InputAction inputAction in ReInput.mapping.ActionsInCategory(mapCategory.name)) {

                string keyName = inputAction.descriptiveName != string.Empty ? inputAction.descriptiveName : inputAction.name;

                var list = new List<ActionElementMap>(selectedMap.AllMaps);
                // list.Sort((x, y) => x.actionId.CompareTo(y.actionId));

                if (inputAction.type == InputActionType.Button) {

                    foreach (ActionElementMap ele in list) {
                        if (ele.actionId != inputAction.id || !inputAction.userAssignable) continue;
                        OneKeyBinding kb = Instantiate(keyBindingPrefab, bd.transform);
                        kb.HandleListening += HandleListening;
                        kb.RenderKey(playerId, inputAction, AxisRange.Positive, selectedMap.controllerType, selectedMap, ele);
                        keyBindingList.Add(kb);
                    }

                } else if (inputAction.type == InputActionType.Axis) {

                    if (controllerType == ControllerType.Joystick) continue;

                    foreach (ActionElementMap ele in selectedMap.AllMaps) {
                        if (ele.actionId != inputAction.id || !inputAction.userAssignable) continue;
                        if (ele.axisType == AxisType.Normal) continue;

                        if (ele.axisContribution != Pole.Positive && ele.axisContribution != Pole.Negative) continue;
                        AxisRange axisRange = ele.axisContribution == Pole.Positive ? AxisRange.Positive : AxisRange.Negative;

                        OneKeyBinding kb = Instantiate(keyBindingPrefab, bd.transform);
                        kb.HandleListening += HandleListening;
                        kb.RenderKey(playerId, inputAction, axisRange, selectedMap.controllerType, selectedMap, ele);
                        keyBindingList.Add(kb);
                    }

                } else {

                    DebugHelper.Log("意外的InputActionType");
                    
                }

            }

        }

        void JudgeNavagation() {

            for (int i = 0; i < this.keyBindingList.Count; i += 1) {

                OneKeyBinding cur = keyBindingList[i];
                Selectable up = null;
                Selectable down = null;
                Selectable left = null;
                Selectable right = null;

                // 上
                if (i - 2 >= 0) {
                    up = keyBindingList[i - 2].button;
                } else if (i - 1 >= 0) {
                    up = keyBindingList[i - 1].button;
                }

                // 下
                if (i + 2 <= keyBindingList.Count - 1) {
                    down = keyBindingList[i + 2].button;
                } else if (i + 1 <= keyBindingList.Count - 1) {
                    down = keyBindingList[i + 1].button;
                }

                // 左
                if (i - 1 >= 0) {
                    left = keyBindingList[i - 1].button;
                }

                // 右
                if (i + 1 <= keyBindingList.Count - 1) {
                    right = keyBindingList[i + 1].button;
                }

                // 倒数两个向下通往DeadZone
                if (i == keyBindingList.Count - 1) {

                    if (controllerType == ControllerType.Keyboard) {

                        down = restoreDefaultButton;

                    } else if (controllerType == ControllerType.Joystick) {

                        down = deadZoneSlider;

                    }

                }

                cur.SetNavagation(up, down, left, right);

            }

            List<Button> buttonGroup = new List<Button>(){
                restoreDefaultButton,
                yesButton,
                noButton
            };

            Navigation nav;

            foreach (Button btn in buttonGroup) {

                if (controllerType == ControllerType.Keyboard) {

                    nav = btn.navigation;
                    nav.selectOnUp = keyBindingList.Count > 0 ? keyBindingList.Last().button : null;
                    btn.navigation = nav;

                } else if (controllerType == ControllerType.Joystick) {

                    nav = btn.navigation;
                    nav.selectOnUp = deadZoneSlider;
                    btn.navigation = nav;

                }

            }

            nav = deadZoneSlider.navigation;
            nav.selectOnUp = keyBindingList.Count > 0 ? keyBindingList.Last().button : null;
            deadZoneSlider.navigation = nav;

        }

        void Clear() {
            foreach (var kv in keyBindingList) {
                Destroy(kv.gameObject);
            }
            foreach (var go in keysBdList) {
                Destroy(go.gameObject);
            }
            keyBindingList.Clear();
            keysBdList.Clear();
            mapper.Stop();
        }

    }
}