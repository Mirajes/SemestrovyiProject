using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerPos;

    [Header("Entity")]
    [SerializeField] private Transform _entitySpawnPos;
    private Entity _currentEntity;
    private float _entitySpawnDelay = 0.5f;

    
}


public class ResourceData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _count;

    [SerializeField] private int _index;

    [SerializeField] private ResourceData _reverseResource; // shifter, skinwalker

    public string Name => _name;
    public Sprite Sprite => _sprite;
    public int Count => _count;
    public int Index => _index;
}

public class Inventory
{

}

public class ItemSlot : MonoBehaviour
{
    private ResourceData _resource;


    public ResourceData Resource => _resource;

    private void OnMouseUpAsButton()
    {
        
    }
}
public class CursorManager : MonoBehaviour // для Raycast
{
    
}

public class Entity
{

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