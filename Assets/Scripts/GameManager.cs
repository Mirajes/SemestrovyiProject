using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private List<ItemData> _itemDatas;
    private CancellationTokenSource _cts;
    private InputSystem_Actions _inputMap;

    public static Action<Entity> ReturnFromCycle;
    public static Action<Statement> ChangeState;

    private void Awake()
    {
        _itemDatas = Resources.LoadAll<ItemData>("Items").ToList(); // zachem
        
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        _inputMap = new();
        _inputMap.Player.Attack.started += GameContext.Instance.CursorManager.OnAttackInput;
        _inputMap.Player.Attack.canceled += GameContext.Instance.CursorManager.OnAttackInput;
        _inputMap.Enable();

        ChangeState += OnStateChanged;
        ReturnFromCycle += OnReturnFromCycle;

        //#region Test
        //GameManager.ChangeState?.Invoke(GameContext.Instance.State);
        //#endregion
    }

    private void OnDisable()
    {
        _inputMap.Player.Attack.started -= GameContext.Instance.CursorManager.OnAttackInput;
        _inputMap.Player.Attack.canceled -= GameContext.Instance.CursorManager.OnAttackInput;
        _inputMap.Dispose();

        ChangeState -= OnStateChanged;
        ReturnFromCycle -= OnReturnFromCycle;
    }

    private void OnReturnFromCycle(Entity entity)
    {
        Destroy(entity.gameObject);
    }

    private void OnStateChanged(Statement state)
    {
        var gameContext = GameContext.Instance;

        switch (state)
        {
            case Statement.Home:
                _cts.Cancel();
                _cts.Dispose();

                gameContext.Player.transform.position = gameContext.HomeManager.PlayerHomePos.position;
                break;
            case Statement.Cycle:
                _cts = new();
                gameContext.CycleManager.EntitySpawnTask(_cts.Token).Forget();

                gameContext.Player.transform.position = gameContext.CycleManager.PlayerSpawnPos.position;
                break;
            default:
                Debug.LogWarning("where state");
                break;
        }
    }
}

//public class EntryPoint : MonoBehaviour
//{
//    [SerializeField] private Button _startButton;

//    private void OnEnable()
//    {
//        _startButton.onClick.AddListener()
//    }

//    private void OnDisable()
//    {
        
//    }

//    private void StartGame()
//    {
//        #region Test
//        ChangeState?.Invoke(GameContext.Instance.State);
//        #endregion
//    }
//}