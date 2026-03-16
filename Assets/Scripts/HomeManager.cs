using UnityEngine;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private Transform _playerHomePos;
    public Transform PlayerHomePos => _playerHomePos;
}
