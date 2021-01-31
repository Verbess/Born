using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace JackUtil {

    public static class CSVHelper {

        public static string[,] LoadToString(string path) {

            string[] str = File.ReadAllLines(path);

            return LoadToString(str);

        }

        public static string[,] LoadToString(string[] csvLines) {

            string[,] csv = null;

            for (int i = 0; i < csvLines.Length; i += 1) {

                string lineStr = csvLines[i];

                string[] oneLineArr = lineStr.Trim().Split(',');

                if (csv == null) {

                    csv = new string[csvLines.Length, oneLineArr.Length];

                }

                for (int j = 0; j < oneLineArr.Length; j += 1) {

                    oneLineArr[j] = oneLineArr[j].Trim();

                    csv[i, j] = oneLineArr[j];

                }

            }

            return csv;

        }
        
    }
}