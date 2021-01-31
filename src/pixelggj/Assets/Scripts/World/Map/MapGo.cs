using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class MapGo : MonoBehaviour {

        DataManager data => Container.Instance.data;

        public string levelId;

        public Vector2 size;

        BlockBase[] blocks;

        void Awake() {
            blocks = GetComponentsInChildren<BlockBase>();
        }

        public LevelModel GenerateLevelModel() {
            LevelModel model = new LevelModel(levelId);
            for (int i = 0; i < blocks.Length; i += 1) {
                BlockBase block = blocks[i];
                if (block != null) {
                    model.blocks.Add(block.GenerateBlockModel());
                }
            }
            return model;
        }

        public void LoadLevelModel(LevelModel model) {
            for (int i = 0; i < blocks.Length; i += 1) {
                BlockBase block = blocks[i];
                if (block != null) {
                    block.LoadBlockModel(model.blocks.Find(value => value.id == block.id));
                    block.Render();
                }
            }
        }

        public void EnterMap() {

            // 如果是第一次游戏
            if (!data.gameData.IsCompleteSceneEvent(SceneEvent.TalkRIP)) {
                RIP rip = GetComponentInChildren<RIP>();
                rip.ActivatedEvent();
            }

        }

    }

}