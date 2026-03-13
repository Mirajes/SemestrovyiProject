using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private Statement _actionContext;

    public void Invoke()
    {
        switch (_actionContext)
        {
            case Statement.Cycle:
                GameManager.StateChange?.Invoke(_actionContext);
                break;
            case Statement.Home:
                GameManager.StateChange?.Invoke(_actionContext);
                break;
            case Statement.Minds:

                break;
            case Statement.Menu:
                GameManager.StateChange?.Invoke(_actionContext);
                break;
        }
    }
}