using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Room : MonoBehaviour
{
    [SerializeField] private bool _isUnlocked = false;
    [SerializeField] private RectTransform _UIWindow;

    public bool IsUnlocked => _isUnlocked;
    
    private void OnMouseUp()
    {
        if (_UIWindow != null)
            _UIWindow.gameObject.SetActive(true);
    }
}