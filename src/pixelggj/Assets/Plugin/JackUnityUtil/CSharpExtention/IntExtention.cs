using System;

namespace JackUtil {

    public static class IntExtention {

        public static bool Between(this int i, int min, int max) {
            return i >= min && i <= max;
        }

        public static int NextListIndex(this int index, int offAxis, int min, int max) {
            if (index + offAxis > max - 1) {
                return min;
            } else if (index + offAxis < 0) {
                return max - 1;
            } else {
                return index + offAxis;
            }
        }

        public static string ToStringChinese(this int i) {
            return i.ToString().Replace('0', '零')
                .Replace('1', '一')
                .Replace('2', '二')
                .Replace('3', '三')
                .Replace('4', '四')
                .Replace('5', '五')
                .Replace('6', '六')
                .Replace('7', '七')
                .Replace('8', '八')
                .Replace('9', '九');
        }

    }

}