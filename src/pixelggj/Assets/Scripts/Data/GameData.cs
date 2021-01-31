using System;
using System.Collections.Generic;
using UnityEngine;
using JackUtil;

namespace PixelGGJNS {

    [Serializable]
    public enum SceneEvent {
        TalkRIP,
        MeetDrawBridge,
    }

    [Serializable]
    public class GameData {

        public Pos pos;
        public InventoryModel[] inventories { get; set; }
        public string currentLevelId { get; set; }
        public Dictionary<string, LevelModel> levelDic { get; set; }
        public Dictionary<SceneEvent, bool> sceneEventDic { get; set; }

        public GameData(Vector2 startPos) {
            pos = new Pos(startPos);
            currentLevelId = "C1L1";
            inventories = new InventoryModel[10];
            levelDic = new Dictionary<string, LevelModel>();
            sceneEventDic = new Dictionary<SceneEvent, bool>();
            foreach (SceneEvent e in Enum.GetValues(typeof(SceneEvent))) {
                sceneEventDic.Add(e, false);
            }
        }

        public bool IsCompleteSceneEvent(SceneEvent e) => sceneEventDic.GetValue(e);
        public void CompleteSceneEvent(SceneEvent e) => sceneEventDic[e] = true;

        public LevelModel GetLevelModel(string levelId) {
            return levelDic.GetValue(levelId);
        }

        public BlockModel GetBlockModel(BlockType block) {
            BlockModel model = null;
            foreach (var kv in levelDic) {
                model = kv.Value.blocks.Find(value => value.id == block.ToInt());
                if (model != null) {
                    return model;
                }
            }
            return model;
        }

        public void InsertLevel(LevelModel level) {
            levelDic.Add(level.levelId, level);
        }

        public void UseInventory(InventoryModel model) {
            for (int i = 0; i < inventories.Length; i += 1) {
                InventoryModel modelInBag = inventories[i];
                if (modelInBag != null && modelInBag.id == model.id) {
                    inventories[i] = null;
                }
            }
        }

        public void SetPos(Vector2 pos) {
            this.pos = new Pos(pos);
        }

        public bool TryAddInventory(InventoryModel inventory) {
            for (int i = 0; i < inventories.Length; i += 1) {
                if (inventories[i] == null) {
                    inventories[i] = inventory;
                    return true;
                }
            }
            return false;
        }

    }

}