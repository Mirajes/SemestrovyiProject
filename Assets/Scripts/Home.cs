using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Home
{
    [SerializeField] private List<Room> _rooms = new();

    [Header("Poses")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _cameraTransform;

    public Transform PlayerTransform => _playerTransform;
    public Transform CameraTransform => _cameraTransform;
}
