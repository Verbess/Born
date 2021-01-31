using System;
using System.Collections.Generic;
using UnityEngine;

namespace JackUtil {

    public class MonoPool<T> where T : MonoBehaviour {

        public T prefab;
        List<T> sets;
        int index;
        int maxCount;

        public MonoPool(T prefab, int maxCount) {

            this.prefab = prefab;
            this.maxCount = maxCount;
            index = 0;

            sets = new List<T>();
            ExtendPool(this.maxCount);

        }

        public void CleanPool() {
            index = 0;
            sets.Clear();
        }

        public void ExtendPool(int count) {

            for (int i = 0; i < count; i += 1) {
                T go = GameObject.Instantiate(prefab);
                Release(go);
                sets.Add(go);
            }

        }

        public T GetObject() {

            index += 1;

            if (index >= sets.Count) {

                T go = GetObject(0, ref index);
                if (go != null) {
                    return go;
                }

            } else {

                T go = GetObject(index, ref index);
                if (go != null) {
                    return go;
                }

            }

            // 无则扩容
            ExtendPool(maxCount);
            return sets[index];

        }

        T GetObject(int fromIndex, ref int index) {

            for (int i = fromIndex; i < sets.Count; i += 1) {

                MonoBehaviour go = sets[i];
                if (!go.isActiveAndEnabled) {
                    index = i;
                    Activated((T)go);
                    return (T)go;
                }

            }

            return null;

        }

        public void Release(T go) {
            go.Hide();
        }

        public void Activated(T go) {
            go.Show();
        }

    }
}