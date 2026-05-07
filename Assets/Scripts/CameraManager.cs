using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    [SerializeField] private Transform _cyclePos;
    [SerializeField] private Transform _homePos;

    public Camera MainCamera => _mainCamera;
    public Transform CyclePos => _cyclePos;
    public Transform HomePos => _homePos;
}