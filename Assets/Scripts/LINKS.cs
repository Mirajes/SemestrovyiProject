using UnityEngine;

public class LINKS : MonoBehaviour
{
    public static LINKS Instance;

    [Header("CycleManager")]
    public Transform CM_EntitySpawnPos;
    public Transform CM_PlayerCyclePos;

    [Header("HomeManager")]
    public Transform HM_PlayerHomePos;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}