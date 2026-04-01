using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private Transform _playerHomePos;
    public Transform PlayerHomePos => _playerHomePos;

    [SerializeField] private List<Room> _rooms = new();

    public void Init()
    {
        foreach (var room in _rooms)
        {
            if (room.IsUnlocked)
                room.gameObject.SetActive(true);
            else 
                room.gameObject.SetActive(false);
        }
    }
}
