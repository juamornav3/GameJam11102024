using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PersistanceManager : MonoBehaviour
{
    public List<PersistentScriptableObject> persistentScripts;
    private bool applicationIsQuitting = false;
    string savePath;

    private void OnEnable()
    {
        savePath = Application.persistentDataPath + "/saves/";
        for (int i = 0; i < persistentScripts.Count; i++)
        {
            var so = persistentScripts[i];
            so.Load();
        }
    }

    private void OnApplicationQuit()
    {
        applicationIsQuitting = true;
        SaveData();
    }

    private void OnDisable()
    {
        if (!applicationIsQuitting)
        {
            SaveData();
        }
    }

    private void SaveData()
    {
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        for (int i = 0; i < persistentScripts.Count; i++)
        {
            var so = persistentScripts[i];
            so.Save();
        }
    }

}
