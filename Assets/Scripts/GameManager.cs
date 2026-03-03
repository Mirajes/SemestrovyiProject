using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(CursorManager))]
public class GameManager : MonoBehaviour
{
    [Header("Main")]
    private InputSystem_Actions _inputMap;
    private CursorManager _cursorManager;
    private StateManager _stateManager = new();
    private GambleManager _gambleManager = new();
    private CancellationTokenSource _cts = new();

    [Header("Support")]
    [SerializeField] private Transform _entitySpawnPos;

    [Header("Inventory")]
    private List<ItemData> _inventoryMain = new();

    private List<ItemData> _cycleOrder = new();
    private int _maxCycleOrderCapacity = 3;

    private List<ItemData> _fightOrder = new();
    private int _maxFightOrder = 3;

    private void Awake()
    {
        _cursorManager = GetComponent<CursorManager>();
        _cursorManager.Init(Camera.main);

        InitControlls();
    }

    private void Start()
    {
        StartGame();
    }

    private void OnDestroy()
    {
        // stop input and cancel any running async loops
        RemoveControlls();
        if (_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }

    #region Controlls
    private void InitControlls()
    {
        _inputMap = new();

        _inputMap.Player.Cursor.performed += _cursorManager.KnowMousePos;

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

        _stateManager.ChangeState("Cycle");

        switch (_stateManager.State)
        {
            case "Cycle":
                CycleTick(_cts.Token).Forget();
                break;
            case "Home":
                break;
            default:
                Debug.Log("where");
                break; 
        }
    }

    // copilot помог
    private async UniTask CycleTick(CancellationToken token)
    {
        try
        {
            // keep running until cancelled
            while (true)
            {
                // iterate over a snapshot so the collection can be modified elsewhere
                foreach (ItemData item in _cycleOrder.ToArray())
                {
                    token.ThrowIfCancellationRequested();

                    EntityData entity = _gambleManager.RollEntity(item);
                    if (entity == null || entity.EntityPrefab == null)
                        continue;

                    GameObject spawned = Instantiate(entity.EntityPrefab, _entitySpawnPos.position, _entitySpawnPos.rotation);

                    // wait until the spawned object is destroyed OR the token is cancelled
                    // UnityEngine.Object == null works for destroyed objects
                    await UniTask.WaitWhile(() => spawned != null, cancellationToken: token);
                }
                // when finished all items, the outer while(true) will restart the foreach
            }
        }
        catch (OperationCanceledException)
        {
            // cancellation requested - exit gracefully
            Console.WriteLine("смотри проблема 0_0");
        }
    }
}
