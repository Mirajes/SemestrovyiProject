using UnityEngine;

public class SelectActionButton : MonoBehaviour
{
    [SerializeField] private e_SelectAction _action;

    public void InvokeAction()
    {
        GameManager.DoAction?.Invoke(_action);
    }
}