using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour // для Raycast
{
    public static Vector2 CursorPos => _cursorPos;
    public static bool IsAttacking => _isAttacking;

    private static Vector2 _cursorPos;
    private static bool _isAttacking = false;

    public void KnowMousePos(InputAction.CallbackContext context)
    {
        _cursorPos = context.ReadValue<Vector2>();
        //Debug.Log(_cursorPos);
    }

    private void Raycast() // прст
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(_cursorPos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        Debug.Log(hit.collider);
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        _isAttacking = context.ReadValueAsButton();
        print(_isAttacking);
    }
}
