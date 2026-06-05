using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    public void MoveCameraTo(Camera camera, Transform endTransform, float time)
    {
        DOTween.Kill(camera.transform);
        camera.transform.DOMove(endTransform.position, time);
    }
}