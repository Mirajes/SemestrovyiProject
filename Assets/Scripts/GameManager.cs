using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _cycleCapacity = 4;
    [SerializeField] private List<ItemType> _cycleInv = new();


    private Dictionary<Item, GameObject> _sourceList = new();
    [SerializeField] private List<GameObject> _interactionObjs = new();

    [SerializeField] private Transform _spawnSpot;

    private bool _isSpawned = false;
    private bool _isAFK = false;

    private void SuddenlyRoll()
    {

    }

    private void SpawnObj(ItemType item)
    {

    }

    private void OnEnable()
    {
        
    }
}

public class Item : ScriptableObject
{
    private string _name;
    private int _id;

    public string Name => _name;
    public int ID => _id;
}

enum ItemType
{
    None,
    Rock,
    Wood,
    Eye
}

enum Status
{
    Dialogue,
    Minigame,
    Home,
    OnWay,
    Afk,
}