using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Extraction : A_Entity
{
    [SerializeField] private float _cursorDamage = 1f;

    private void OnMouseOver()
    {
        if (!_IsMouseHolding) return;

        TakeDamage(_cursorDamage * _CursorSpeed * Time.deltaTime);
    }
}
