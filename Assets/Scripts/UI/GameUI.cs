using UnityEngine;

public class GameUI : MonoBehaviour
{
    public EntityHealthBar HeathBar => _entityHealthBar;

    [SerializeField] private EntityHealthBar _entityHealthBar;
}
