using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private bool _isUnlocked = false;

    public bool IsUnlocked => _isUnlocked;
}