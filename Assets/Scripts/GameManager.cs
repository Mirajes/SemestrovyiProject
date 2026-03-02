using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Main")]
    private InputSystem_Actions _inputMap;
    private CursorManager _cursor;
    private StateManager _stateManager = new();

    [Header("Inventory")]
    private List<ItemData> _inventoryMain;

    private List<ItemData> _cycleOrder = new();
    private int _maxCycleOrderCapacity = 3;

    private List<ItemData> _fightOrder = new();
    private int _maxFightOrder = 3;

    private void Awake()
    {
        _cursor = new(Camera.main);

        InitControlls();
    }

    private void Start()
    {
        StartGame();
    }

    private void OnDestroy()
    {
        RemoveControlls();
    }

    #region Controlls
    private void InitControlls()
    {
        _inputMap = new();

        _inputMap.Player.Cursor.performed += _cursor.KnowMousePos;

        _inputMap.Enable();
    }
    private void RemoveControlls()
    {
        _inputMap.Disable();
        _inputMap.Dispose();
    }
    #endregion

    private void StartGame()
    {
        ItemData someItem = Resources.Load<ItemData>("Items/Instrument");
        _cycleOrder.Add(someItem);



        switch (_stateManager.State)
        {
            case "Cycle":
                break;
            case "Home":
                break;
            default:
                Debug.Log("where");
                break;
        }
    }

    private async UniTask GameTick()
    {

    }
}

public class GambleManager
{
    public void RollEntity(ItemData item)
    {
        float totalChance = 0f;
        foreach (float chance in item._chanceMaker.Values)
            totalChance += chance;

        float random = Random.value;        
    }
}

public class StateManager
{
    private string _state;
    public string State => _state;

    public void ChangeState(string state) { _state = state; }
}

public class GUI : MonoBehaviour
{
    
}