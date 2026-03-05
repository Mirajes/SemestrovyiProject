using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntityData Data => _data;

    private EntityData _data;
    [SerializeField] private float _health = 3f;

    public void Init(EntityData data) {  _data = data; }

    private void OnDestroy()
    {
        
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        print("mouse came out");
    }
}
