using UnityEngine;

public abstract class A_Singleton<T> : MonoBehaviour where T : class
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Init();
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this as T)
        {
            DeInit();
            Instance = null;
        }
    }

    protected virtual void Init() 
    {

    }

    protected virtual void DeInit()
    {

    }
}