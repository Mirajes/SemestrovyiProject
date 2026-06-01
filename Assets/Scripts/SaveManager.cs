using System;
using UnityEngine;

public class SaveManager // eto dolzhno bit' peredelano pod Yandex.Games
{
    public static Action<SaveData> OnSave;
    public static Action<SaveData> OnLoad;

    public void Save()
    {
        SaveData data = new();
        OnSave?.Invoke(data);

        string save = JsonUtility.ToJson(data);
        //Debug.Log(save);
        PlayerPrefs.SetString("Save", save);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        var json = PlayerPrefs.GetString("Save", "");
        if (string.IsNullOrEmpty(json))
            return;

        SaveData data = new();
        data = JsonUtility.FromJson<SaveData>(json);

        OnLoad?.Invoke(data);
    }
}
