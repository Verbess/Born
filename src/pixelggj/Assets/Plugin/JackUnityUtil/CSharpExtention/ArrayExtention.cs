using System;

namespace JackUtil {

    public static class ArrayExtention {

        public static T[] Shuffle<T>(this T[] arr, Random random = null) {

            if (random == null) random = new Random();

            for (int i = 0; i < arr.Length; i += 1) {

                T cur = arr[i];

                int _rdIndex = random.Next(arr.Length);

                arr[i] = arr[_rdIndex];

                arr[_rdIndex] = cur;

            }

            return arr;

        }

        public static bool TryGetEmptySlotIndex<T>(this T[] arr, out int index) {

            for (int i = 0; i < arr.Length; i += 1) {
                T cur = arr[i];
                if (cur == null) {
                    index = i;
                    return true;
                }
            }

            index = -1;
            return false;

        }

        public static T[] Sort<T>(this T[] arr) {

            if (arr == null || arr.Length == 0) {
                return arr;
            }
            if (arr[0] as IComparable<T> == null) {
                DebugHelper.LogError("未实现 IComparable<T>");
                return arr;
            }

            for (int i = 0; i < arr.Length; i += 1) {
                for (int j = 0; j < arr.Length; j += 1) {
                    T t = arr[j];
                    IComparable<T> _compare = t as IComparable<T>;
                    if (j + 1 < arr.Length) {
                        if (_compare.CompareTo(arr[j + 1]) == 1) {
                            arr[j] = arr[j + 1];
                            arr[j + 1] = t;
                        }
                    }
                    
                }
            }

            return arr;

        }

    }
}