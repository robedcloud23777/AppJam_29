using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using UnityEditor.Overlays;

public class SaveFileManager : MonoBehaviour
{
    [SerializeField] string fileLocation, fileExtension;
    private void Awake()
    {
        SaveData data = Load();
        if(data != null)
        {
            foreach (var i in GetSavables()) i.Load(data);
        }
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    public void SaveGame()
    {
        SaveData data = new();
        foreach(var i in GetSavables()) i.Save(data);
        Save(data);
    }
    void Save(SaveData data)
    {
        string path = Path.Combine(fileLocation, "Save" + fileExtension);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(JsonUtility.ToJson(data, true));
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Save Path:" + path + "\n" + e);
        }
    }
    SaveData Load()
    {
        string path = Path.Combine(fileLocation, "Save" + fileExtension);
        SaveData loadedData = null;
        if (File.Exists(path))
        {
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        loadedData = JsonUtility.FromJson<SaveData>(reader.ReadToEnd());
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Load Path:" + path + "\n" + e);
            }
        }
        return loadedData;
    }

    IEnumerable<ISavable> GetSavables()
    {
        return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISavable>();
    }
}