using UnityEngine;

public class SelectActionButton : MonoBehaviour
{
    [SerializeField] private SelectAction _action;

    public void InvokeAction()
    {
        GameManager.DoAction?.Invoke(_action);
    }
}