using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SelectActionWindow : MonoBehaviour
{
    [SerializeField] private Transform _window;

    private void OnMouseDown()
    {
        _window.gameObject.SetActive(true);
    }

    private void OnMouseUp()
    {
        if (GameManager.Instance.CursorManager.Raycast(out RaycastHit2D hit).collider != null)
        {
            hit.collider.TryGetComponent<SelectActionButton>(out SelectActionButton button);
            if (button != null)
                button.InvokeAction();
        }

        _window.gameObject.SetActive(false);
    }
}
