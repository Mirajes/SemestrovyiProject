using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _contextMenu;
    [SerializeField] private SpriteRenderer _head;
    private void OnMouseDown()
    {
        
        _contextMenu.SetActive(true);
    }

    private void OnMouseUp()
    {

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(CursorManager.CursorPos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null)
        {
            hit.collider.TryGetComponent<Interactable>(out Interactable actionButton);

            if (actionButton != null)
            {
                actionButton.Invoke();

            }
        }
        _contextMenu.SetActive(false);
    }

    private void OnMouseDrag()
    {

    }
}

