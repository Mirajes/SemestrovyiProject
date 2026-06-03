public class InputHandler
{
    private InputSystem_Actions _inputs;
    public InputSystem_Actions Inputs => _inputs;

    public void Init()
    {
        _inputs = new();
    }

    public void InitInputs(CursorManager cursorManager)
    {
        _inputs.Player.Cursor.performed += cursorManager.OnCursorInput;
        _inputs.Player.Cursor.canceled += cursorManager.OnCursorInput;

        _inputs.Player.Attack.performed += cursorManager.OnHoldInput;
        _inputs.Player.Attack.canceled += cursorManager.OnHoldInput;
    }

    public void RemoveInputs(CursorManager cursorManager)
    {
        _inputs.Player.Cursor.performed -= cursorManager.OnCursorInput;
        _inputs.Player.Cursor.canceled -= cursorManager.OnCursorInput;

        _inputs.Player.Attack.performed -= cursorManager.OnHoldInput;
        _inputs.Player.Attack.canceled -= cursorManager.OnHoldInput;
    }
}