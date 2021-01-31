using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;

namespace JackUtil {

    public static class FileHelper {

        static bool allowCatch = false;

        public static void CreateDirIfNorExist(string _dirPath) {
            if (!Directory.Exists(_dirPath)) {
                Directory.CreateDirectory(_dirPath);
            }
        }

        public static void SaveFileBinary(object _object, string _dirPath, string _fileName) {
            CreateDirIfNorExist(_dirPath);
            SaveFileBinary(_object, _dirPath + _fileName);
        }

        public static void SaveFileText(string txt, string path) {

            using (StreamWriter sw = File.CreateText(path)) {
                sw.Write(txt);
            }

        }

        public static string LoadTextFromFile(string path) {

            using (StreamReader sr = new StreamReader(path)) {
                return sr.ReadToEnd();
            }

        }

        public static void SaveFileBinary<T>(T obj, string url) {

            // 二进制流
            BinaryFormatter bf = new BinaryFormatter();

            FileStream fs;

            // 文件流
            if (!File.Exists(url)) {

                using (fs = File.Create(url)) {

                    bf.Serialize(fs, obj);

                }

            } else {

                using (fs = File.Open(url, FileMode.Create)) {

                    bf.Serialize(fs, obj);

                }

            }
            
        }

        public static T LoadFileFromBinary<T>(string filePath) {

            if (!File.Exists(filePath)) {
                return default;
            }

            BinaryFormatter bf = new BinaryFormatter();

            using (FileStream fs = new FileStream(filePath, FileMode.Open)) {

                T t = default;

                try {

                    t = (T)bf.Deserialize(fs);

                } catch(Exception e) {

                    if (allowCatch) {
                        DebugHelper.Log(e);
                    }

                    return default;

                }

                return t;

            }
            
        }

        public static void DeleteAllFilesInDirUnsafe(string _dirPath) {

            string[] _files = Directory.GetFiles(_dirPath);
            for (int i = 0; i < _files.Length; i += 1) {
                string _path = _files[i];
                File.Delete(_path);
            }

        }

    }
}