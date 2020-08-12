using System;
using System.IO;
using UnityEngine;

namespace KaynirGames.Tools
{
    public static class JsonLoader
    {
        /// <summary>
        /// Сохранить данные в json-файл.
        /// </summary>
        public static void SaveData(string filePath, object data)
        {
            string content = JsonUtility.ToJson(data);

            if (!File.Exists(filePath))
            {
                FileStream fs = File.Create(filePath);
                fs.Close();
            }

            File.WriteAllText(filePath, content);
        }
        /// <summary>
        /// Загрузить данные из json-файла.
        /// </summary>
        public static object LoadData(string filePath, Type dataType)
        {
            string json = string.Empty;

            if (File.Exists(filePath))
            {
                json = File.ReadAllText(filePath);
            }

            if (json == string.Empty)
            {
                return null;
            }

            return JsonUtility.FromJson(json, dataType);
        }
    }
}
