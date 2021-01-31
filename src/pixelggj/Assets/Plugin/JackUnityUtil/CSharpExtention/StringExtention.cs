using System;
using System.Text;
using System.Text.RegularExpressions;

namespace JackUtil {

    public static class StringExtention {

        public static string WrapTextByLineSize(this string s, int lineSize) {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i += lineSize) {
                if (lineSize > s.Length - i) {
                    lineSize = s.Length - i;
                }
                sb.AppendLine(s.Substring(i, lineSize));
            }
            return sb.ToString().TrimEnd();
        }

        public static string ReplaceNumberToChinese(this string s) {
            return s.Replace('0', '零')
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

        public static MatchCollection GetMatchesIntegerBetweenTowChar(this string str, string startChar, string endChar) {
            Regex reg = new Regex(@"[" + startChar + "]+[0-9]+[" + endChar + "]");
            return reg.Matches(str);
        }

        public static MatchCollection GetMatchesLettersBetweenTwoChar(this string str, string startChar, string endChar) {
            Regex reg = new Regex(@"[" + startChar + "]+[a-zA-Z]+[" + endChar + "]");
            return reg.Matches(str);
        }

        public static string ToJoystickSimpleString(this string s) {
            s = s.Replace("Left Shoulder", "LB");
            s = s.Replace("Right Shoulder", "RB");
            return s;
        }

    }
}