using UnityEngine;

public class GameContext : Singleton<GameContext>
{
    public Player Player => _player;
    public GameManager GameManager => _gameManager;
    public CycleManager CycleManager => _cycleManager;
    public HomeManager HomeManager => _homeManager;
    public CursorManager CursorManager => _cursorManager;
    public GambleLogic Gamble => _gamble;

    public Statement State => _state;


    [Header("Links")]
    [SerializeField] private Player _player;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private CycleManager _cycleManager;
    [SerializeField] private HomeManager _homeManager;
    [SerializeField] private CursorManager _cursorManager;
    private GambleLogic _gamble = new();

    [Header("Vars")]
    [SerializeField] private Statement _state;
}
