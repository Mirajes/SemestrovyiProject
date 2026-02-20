using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour // для Raycast
{
    private Camera _mainCamera;

    private Vector2 _cursorPos;

    private void OnEnable()
    {
        _mainCamera = Camera.main;
        _gameManager = FindAnyObjectByType<GameManager>();

        _gameManager.Delegate += CheckForInteract;
    }

    private GameManager _gameManager;

    public void KnowMousePos(InputAction.CallbackContext callback)
    {
        _cursorPos = callback.ReadValue<Vector2>();
        print(_cursorPos);
    }

    private void CheckForInteract()
    {
        

        //RaycastHit2D hit = Physics2D.Raycast();
    }

}

//public class StateManager
//{

//}


//public class AFKHandler { } // когда игра закрыта

//public class Inventory { } // 

//public class Cycle { } // 

//public class GUI : MonoBehaviour
//{

//}

//public class NPC { }