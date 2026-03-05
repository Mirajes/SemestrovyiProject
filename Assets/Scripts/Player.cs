using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _contextMenu;
    [SerializeField] private SpriteRenderer _head;
    private void OnMouseDown()
    {
        print("on me");
        
    }

    private void OnMouseUp()
    {
        print("stopped");

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(CursorManager.CursorPos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.transform.TryGetComponent<Interactable>(out Interactable button))
        {
            button.OpenMenu("sosal");
            _contextMenu.SetActive(false);
        }
    }

    private void OnMouseDrag()
    {
        print("dug");
    }
}

public class Interactable : MonoBehaviour
{
    public void OpenMenu(string menuName)
    {

    }
}