using System.Collections;
using MelonLoader;
using UnityEngine;

namespace AdvancedLogSystem
{
    public static class AdvancedLogger
    {
        public static int NumberOfLogs = 1;
        public static string CurrentLogFile = "log.txt";
        public static bool IsLoggingEnabled = false;

        private static readonly string[] CyrillicUpperCase =
        {
            "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", 
            "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я"
        };

        private static readonly string[] CyrillicLowerCase =
        {
            "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", 
            "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я"
        };

        private static readonly string[] LatinUpperCase =
        {
            "A", "B", "V", "G", "D", "E", "Yo", "Zh", "Z", "I", "Y", "K", "L", "M", "N", "O", "P", 
            "R", "S", "T", "U", "F", "Kh", "Ts", "Ch", "Sh", "Shch", "\"", "Y", "'", "E", "Yu", "Ya"
        };

        private static readonly string[] LatinLowerCase =
        {
            "a", "b", "v", "g", "d", "e", "yo", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p", 
            "r", "s", "t", "u", "f", "kh", "ts", "ch", "sh", "shch", "\"", "y", "'", "e", "yu", "ya"
        };

        public static void LogMessage(string message)
        {
            MelonLogger.Msg(message);
            WriteDataToFile(message);
        }

        public static void WriteDataToFile(string text)
        {
            using (StreamWriter streamWriter = new($"UserData\\AdvancedLogSystem\\{CurrentLogFile}", 
                true))
            {
                streamWriter.WriteLine(text);
                streamWriter.Close();
            }
        }

        public static void CreateNewLogFile()
        {
            NumberOfLogs++;
            CurrentLogFile = $"log{NumberOfLogs}.txt";
        }

        public static void DeleteAllLogFiles()
        {
            if (!Directory.Exists("UserData\\AdvancedLogSystem"))
                return;

            foreach (string filePath in Directory.GetFiles("UserData\\AdvancedLogSystem\\"))
                File.Delete(filePath);
        }

        //Translit by Vantablack
        public static string Translit(string input)
        {
            for (int i = 0; i < CyrillicUpperCase.Length; i++)
            {
                input = input.Replace(CyrillicUpperCase[i], LatinUpperCase[i]);
                input = input.Replace(CyrillicLowerCase[i], LatinLowerCase[i]);
            }

            return input;
        }
    }
}
