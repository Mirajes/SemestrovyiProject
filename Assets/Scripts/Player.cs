using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _contextMenu;
    [SerializeField] private SpriteRenderer _head;
    private void OnMouseDown()
    {
        print("on me");
        
        _contextMenu.SetActive(true);
    }

    private void OnMouseUp()
    {
        print("stopped");

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(CursorManager.CursorPos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null)
        {
            hit.collider.TryGetComponent<Interactable>(out Interactable button);

            if (button != null)
            {
                button.Invoke();

                print("mouse");
            }
        }
        _contextMenu.SetActive(false);
    }

    private void OnMouseDrag()
    {
        print("dug");
    }
}

