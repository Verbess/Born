using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JackUtil;
using Newtonsoft.Json;
using Newtonsoft;

namespace PixelGGJNS {

    public class DataManager : MonoBehaviour {

        public static string path;
        public LocalizationDao localizationDao;
        public GameData gameData;
        public GameObject mapEditor;
        public Transform startPos;

        List<MapGo> maps;

        void Awake() {

            path = Application.dataPath + "/data";

            localizationDao.Init(LanguageType.CN);

            maps = new List<MapGo>();
            MapGo[] mapArr = mapEditor.GetComponentsInChildren<MapGo>();
            foreach (MapGo map in mapArr) {
                maps.Add(map);
            }

        }

        public void NewGame() {

            gameData = new GameData(startPos.position);
            for (int i = 0; i < maps.Count; i += 1) {
                MapGo map = maps[i];
                if (map != null) {
                    gameData.InsertLevel(map.GenerateLevelModel());
                }
            }
            RenderData();
            SaveData();

        }

        public void LoadGame() {
            gameData = LoadData();
            RenderData();
        }

        void RenderData() {
            for (int i = 0; i < maps.Count; i += 1) {
                MapGo map = maps[i];
                if (map != null) {
                    map.LoadLevelModel(gameData.levelDic.GetValue(map.levelId));
                }
            }
        }

        void Start() {
            LocalizationDao.ChangeLangHandle?.Invoke(localizationDao.lang);
        }

        public void SetCurrentMap(MapGo map) {
            gameData.currentLevelId = map.levelId;
        }

        public MapGo GetCurrentMap() {
            for (int i = 0; i < maps.Count; i += 1) {
                MapGo map = maps[i];
                if (map != null && map.levelId == gameData.currentLevelId) {
                    return map;
                }
            }
            return null;
        }

        GameData LoadData() {
            GameData data = FileHelper.LoadFileFromBinary<GameData>(path);
            return data;
        }

        public bool IsExistData() {
            return File.Exists(path);
        }

        public void SaveData() {
            FileHelper.SaveFileBinary(gameData, path);
        }

    }
}