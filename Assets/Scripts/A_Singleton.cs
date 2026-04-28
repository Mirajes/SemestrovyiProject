using UnityEngine;

public abstract class A_Singleton<T> : MonoBehaviour where T : class
{
    public T Instance { get; private set; }

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
    }
}
