using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _orthographicSize = 5f;

    [SerializeField] private Transform _cyclePos;
    [SerializeField] private Transform _homePos;
}