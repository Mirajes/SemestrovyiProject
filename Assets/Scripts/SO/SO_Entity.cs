using UnityEngine;

[CreateAssetMenu(fileName = "Entity_SO", menuName = "Scriptable Objects/Entity_SO")]
public class SO_Entity : ScriptableObject
{
    [SerializeField] private float _HEALTH_BASE;

    public float BaseHealth => _HEALTH_BASE;
}
