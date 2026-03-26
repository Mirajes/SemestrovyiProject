using Cysharp.Threading.Tasks;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera MainCamera => _mainCamera;
    public Transform CyclePos => _cyclePos;
    public Transform HomePos => _homePos;

    [SerializeField] private Camera _mainCamera;
    //[SerializeField] private float _orthographicSize = 5f;
    [SerializeField] private float _lerpT = 0.1f;
    //[SerializeField] private float _pullStrenght = 5f;

    [SerializeField] private Transform _cyclePos;
    [SerializeField] private Transform _homePos;

    public async UniTask FollowTarget(Transform target) // + token
    {
        while (_mainCamera.transform.position != target.position)
        {
            _mainCamera.transform.position = Vector3.Lerp(
            _mainCamera.transform.position,
            target.position,
            _lerpT);

            await UniTask.Delay(10);
        }
    }

    //public void UpdatePullOffset(Vector2 cursorPosition, Vector2 screenSize, out Vector2 targetOffset)
    //{
    //    Vector2 screenCenter = screenSize * 0.5f;
    //    Vector2 delta = cursorPosition - screenCenter;
    //    Vector2 direction = delta.normalized;

    //    targetOffset = -new Vector3(direction.x, direction.y, 0f) * _pullStrenght;
    //}
}