using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class WorldManager : MonoBehaviour {

        CameraManager cameraManager => Container.Instance.cameraManager;
        DataManager data => Container.Instance.data;
        UIManager ui => Container.Instance.ui;

        public Actor actor;
        public MapGo currentMap;
        public GameObject mouseAnchor;

        void Awake() {
            actor.Hide();
        }

        public void Execute() {

            if (actor == null || currentMap == null) {
                return;
            }

            actor.Execute();

            if (actor.transform.position.x > (currentMap.size.x + currentMap.transform.position.x) * 0.7f) {
                ui.hudPage.inventoryGroup.SetLeft(true);
            } else {
                ui.hudPage.inventoryGroup.SetLeft(false);
            }

            bool t = EventSystem.current.IsPointerOverGameObject();
            if (t) {
                // print("点到UI了");
                return;
            }

            if (Input.GetMouseButtonDown(0) && !actor.isLock) {
                Vector3 mousePos = Input.mousePosition;
                Vector2 worldPos = cameraManager.cam.GetMouseWorldPosition(mousePos);
                mouseAnchor.transform.position = worldPos;
                actor.SetTarget(mouseAnchor.transform.position);
                RaycastHit2D[] hits = Physics2D.RaycastAll(worldPos, Vector2.zero);
                if (hits.Length > 0) {
                    bool hasBlock = false;
                    for (int i = 0; i < hits.Length; i += 1) {
                        RaycastHit2D hit = hits[i];
                        if (hit.collider != null) {
                            GameObject go = hit.collider.gameObject;
                            BlockBase block = go.GetComponent<BlockBase>();
                            if (block != null) {
                                hasBlock = true;
                                actor.SetExchangeBlock(block);
                            }
                        }
                    }
                    if (!hasBlock) {
                        actor.SetExchangeBlock(null);
                    }
                } else {
                    actor.SetExchangeBlock(null);
                }
            }

            // if (Input.GetKeyUp(KeyCode.Alpha9)) {
            //     InventoryGroup group = ui.hudPage.inventoryGroup;
            //     foreach (InventoryType value in Enum.GetValues(typeof(InventoryType))) {
            //         group.InsertInventory(new InventoryModel(value));
            //     }
            // }

        }

        public void FixedExecute() {
            actor?.FixedExecute();
        }

        MapGo LoadMap() {
            return currentMap;
        }

        public void StartGame() {

            Pause(false);

            actor.Show();
            currentMap = data.GetCurrentMap();
            actor.transform.position = data.gameData.pos.ToV3();
            mouseAnchor.transform.position = actor.transform.position;

            SetFollowingLimited();

            currentMap.EnterMap();

        }

        public void EnterMap(MapGo targetMap) {
            currentMap = targetMap;
            data.SetCurrentMap(currentMap);
            SetFollowingLimited();
        }

        public void Pause(bool isPause) {
            // Pause
            // actor?.Pause(isPause);
            Time.timeScale = isPause ? 0 : 1;
        }

        void SetFollowingLimited() {
            cameraManager.SetFollowingLimited(actor.gameObject, currentMap.transform.position, currentMap.size);
        }

    }

}