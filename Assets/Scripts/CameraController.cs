using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera MainCamera => _mainCamera;
    public Vector3 CameraHomePos => _cameraHomePos;
    public Vector3 CameraCyclePos => _cameraCyclePos;


    private Camera _mainCamera;
    [SerializeField] private Vector3 _cameraHomePos;
    [SerializeField] private Vector3 _cameraCyclePos;

    public void Init()
    {
        _mainCamera = Camera.main;
    }
}