using UnityEngine;

public class Room : MonoBehaviour
{
    public bool IsUnlocked => _isUnlocked;
    
    [SerializeField] private bool _isUnlocked = false;

}