using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Entity : MonoBehaviour
{
    public EntityData Data => _data;

    private EntityData _data;
    [SerializeField] private float _health = 3f;

    private float _passedDistance = 0f;


    public void Init(EntityData data) { _data = data; }

    private void OnDestroy()
    {
        
    }



    private void OnMouseDown()
    {
        print("show info");
    }

    private void OnMouseUp()
    {
        if (CursorManager.IsAttacking)
            _passedDistance = 0f;
        print("stopped");
    }

    private void OnMouseDrag()
    {
        if (!CursorManager.IsAttacking) return;

        //_passedDistance += CursorManager.CursorPos.
    }

    private void OnMouseEnter()
    {
        if (!CursorManager.IsAttacking) return;

        print("oh on entity");
    }

    private void OnMouseExit()
    {
        if (!CursorManager.IsAttacking) return; // stopping there _passedDistance = 0f;

        print("ouch");
    }
}
