using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private MenuAction _actionContext;

    public void Invoke()
    {
        switch (_actionContext)
        {
            case MenuAction.Run:
                break;
            case MenuAction.Home:
                break;
            case MenuAction.Minds:
                break;
            case MenuAction.Menu:
                break;
        }
    }
}

