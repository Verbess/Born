using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace JackUtil {

    public static class DictionaryExtention {

        public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> d, Action<TKey, TValue> action) {
            if (action == null) {
                throw new Exception("需传入 action 参数");
            }
            foreach (var kv in d) {
                action(kv.Key, kv.Value);
            }
        }

        public static KeyValuePair<TKey, TValue> FindKV<TKey, TValue>(this Dictionary<TKey, TValue> d, Predicate<TKey> match) {
            if (match == null) {
                throw new Exception("需传入 match 参数");
            }
            foreach (var kv in d) {
                if (match(kv.Key)) {
                    return kv;
                }
            }
            return default;
        }

        public static KeyValuePair<TKey, TValue> FindKV<TKey, TValue>(this Dictionary<TKey, TValue> d, Predicate<TValue> match) {
            if (match == null) {
                throw new Exception("需传入 match 参数");
            }
            foreach (var kv in d) {
                if (match(kv.Value)) {
                    return kv;
                }
            }
            return default;
        }

        public static TKey FindKey<TKey, TValue>(this Dictionary<TKey, TValue> d, Predicate<TKey> match) {
            if (match == null) {
                throw new Exception("需传入 match 参数");
            }
            foreach (var kv in d) {
                if (match(kv.Key)) {
                    return kv.Key;
                }
            }
            return default;
        }

        public static TValue FindValue<Tkey, TValue>(this Dictionary<Tkey, TValue> d, Predicate<TValue> match) {
            if (match == null) {
                throw new Exception("需传入 match 参数");
            }
            foreach (var kv in d) {
                if (match(kv.Value)) {
                    return kv.Value;
                }
            }
            return default;
        }

        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key, TValue value = default(TValue)) {
            if (d.ContainsKey(key)) {
                return d[key];
            } else {
                return value;
            }
        }

        public static TValue GetValue<TKey, TValue>(this SortedDictionary<TKey, TValue> d, TKey key, TValue value = default(TValue)) {
            return d.ContainsKey(key) ? d[key] : value;
        }

        public static bool TryRemove<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key) {
            if (d.ContainsKey(key)) {
                d.Remove(key);
                return true;
            } else {
                return false;
            }
        }

        public static bool TryRemove<TKey, TValue>(this SortedDictionary<TKey, TValue> d, TKey key) {
            if (d.ContainsKey(key)) {
                d.Remove(key);
                return true;
            } else {
                return false;
            }
        }

        public static Dictionary<TKey, TValue> AddIfNotExists<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key, TValue value) {
            if (!d.ContainsKey(key)) {
                d.Add(key, value);
            }
            return d;
        }

        public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key, TValue value) {
            if (d.ContainsKey(key)) {
                d[key] = value;
            } else {
                d.Add(key, value);
            }
            return d;
        }

        public static SortedDictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this SortedDictionary<TKey, TValue> d, TKey key, TValue value) {
            if (d.ContainsKey(key)) {
                d[key] = value;
            } else {
                d.Add(key, value);
            }
            return d;
        }

        public static List<TKey> GetKeyList<TKey, TValue>(this Dictionary<TKey, TValue> dic) {
            return new List<TKey>(dic.Keys);
        }

        public static List<TValue> GetValueList<TKey, TValue>(this Dictionary<TKey, TValue> d) {
            return new List<TValue>(d.Values);
        }

        public static List<TValue> GetValueList<TKey, TValue>(this SortedDictionary<TKey, TValue> d) {
            return new List<TValue>(d.Values);
        }

        public static TKey GetKeyWithValue<TKey, TValue>(this Dictionary<TKey, TValue> d, TValue value) {
            foreach (var kv in d) {
                if (kv.Value.Equals(value)) {
                    return kv.Key;
                }
            }
            return default;
        }

        public static TKey GetKeyWithValue<TKey, TValue>(this SortedDictionary<TKey, TValue> d, TValue value) {
            foreach (var kv in d) {
                if (kv.Value.Equals(value)) {
                    return kv.Key;
                }
            }
            return default;
        }

        public static string ToJsonString<Tkey, TValue>(this Dictionary<Tkey, TValue> dic) {
            string s = "{";
            int c = 0;
            foreach (KeyValuePair<Tkey, TValue> kv in dic) {
                s += kv.Key.ToString() + ":";
                s += kv.Value.ToString();
                if (c < dic.Count - 1) {
                    s += ",";
                }
                c += 1;
            }
            s += "}";
            return s;
        }

    }
}