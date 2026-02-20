using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class NPC_SaveTest : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private string _name;

    
}

public class SaveManagerTest : MonoBehaviour
{
    private Dictionary<string, object> _saveData = new();

    public void Save()
    {
        //var saveObjs = GameObject.FindAnyObjectByType<>
    }
}
public interface ISavable
{
    public object SaveData();
    //public 
}