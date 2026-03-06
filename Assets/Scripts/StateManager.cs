// рудимент

public class StateManager
{
    private string _state;
    public string State => _state;

    public void ChangeState(string state) { _state = state; }
}
