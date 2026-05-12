using UnityEngine;

[CreateAssetMenu(fileName = "GameVariables", menuName = "Scriptable Objects/GameVariables")]
public class GameVariables : ScriptableObject
{
    [Header("Sanity")]
    [SerializeField] private float _sanityCapacity_BASE = 100f;
    [SerializeField] private float _sanityRecoveryCD = 3f;
    [SerializeField] private float _sanityRecoveryAmount = 10f;

    [Header("Player")]
    [SerializeField] private int _maxCycleOrderCapacity = 1;
    [SerializeField] private int _maxFightOrderCapacity = 1;

    public float SanityCapacity_BASE => _sanityCapacity_BASE;
    public float SanityRecoveryCD => _sanityRecoveryCD;
    public float SamplingRecoveryAmount => _sanityRecoveryAmount;

    public int MaxCycleOrderCapacity => _maxCycleOrderCapacity;
    public int MaxFightOrderCapacity => _maxFightOrderCapacity;
}
