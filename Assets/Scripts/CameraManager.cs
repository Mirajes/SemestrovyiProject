using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    [SerializeField] private Transform _homePos;
    [SerializeField] private Transform _cyclePos;

    public Camera MainCamera => _mainCamera;
    public Transform HomePos => _homePos;
    public Transform CyclePos => _cyclePos;
}