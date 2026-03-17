using UnityEngine;

public abstract class A_Singleton<T> : MonoBehaviour where T : class
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = this as T;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        Init();
    }

    private void OnDestroy()
    {
        print($"{Instance} is destroying");

        if (Instance == this as T)
            Instance = null;
    }

    protected virtual void Init() 
    {

    }

    protected virtual void DeInit()
    {

    }
}