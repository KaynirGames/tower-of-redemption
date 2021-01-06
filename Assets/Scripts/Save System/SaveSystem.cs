using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string SAVE_FOLDER = $"{Application.persistentDataPath}/Saves/";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void SaveData(object data, string fileName = "AutoSave.sav")
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = GetFilePath(fileName);

        using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
        {
            formatter.Serialize(stream, data);
        }
    }

    public static object LoadData(string fileName = "AutoSave.sav")
    {
        string path = GetFilePath(fileName);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                return formatter.Deserialize(stream);
            }
        }

        return null;
    }

    public static void DeleteSaveFile(string fileName = "AutoSave.sav")
    {
        string path = GetFilePath(fileName);

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    private static string GetFilePath(string fileName)
    {
        return $"{SAVE_FOLDER}{fileName}";
    }
}
