using UnityEngine;
using System.IO;

namespace HimeLib
{
    public class WriteLocalLog
    {
        public static string path = Application.dataPath + "/Record.txt";
        public static int RecordNumber;

        /// <summary>
        /// 寫入文字至專案中的Record.txt檔案
        /// </summary>
        public static void WriteString(string str)
        {
            //Write some text to the Record.txt file
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(str);
            writer.Close();
            RecordNumber++;
        }

        /// <summary>
        /// 讀取專案中的Record.txt檔案
        /// </summary>
        public static void ReadString(string str)
        {
            //Read the text from directly from the Record.txt file
            StreamReader reader = new StreamReader(path);
            Debug.Log(reader.ReadToEnd());
            reader.Close();
        }
    }
}