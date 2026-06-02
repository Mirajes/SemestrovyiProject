using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UI_Inventory _inventory;

    private void OnEnable()
    {
        #region Debug
        PlayerData.ChangedPlayerState += UpdateButton_debug;

        _toLoop.onClick.AddListener(ToLoop);
        _toHome.onClick.AddListener(ToHome);
        #endregion
    }

    private void OnDisable()
    {
        #region Debug
        PlayerData.ChangedPlayerState -= UpdateButton_debug;

        _toLoop.onClick.RemoveAllListeners();
        _toHome.onClick.RemoveAllListeners();
        #endregion
    }

    #region Debug
    [Header("Debug")]
    [SerializeField] private Button _toLoop;
    [SerializeField] private Button _toHome;

    public void UpdateButton_debug(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.InLoop:
                _toLoop.interactable = false;
                _toHome.interactable = true;
                break;
            case PlayerState.InHome:
                _toLoop.interactable = true;
                _toHome.interactable = false;
                break;
            default:
                Debug.Log($"[UIManager] - where {state}");
                break;
        }
    }

    private void ToLoop()
    {
        GameManager.ChangeState?.Invoke(PlayerState.InLoop);
    }

    private void ToHome()
    {
        GameManager.ChangeState?.Invoke(PlayerState.InHome);
    }
    #endregion
}
