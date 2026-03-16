using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : class
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        print(typeof(T));

        if (Instance == null)
            Instance = this as T;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
        Init();
        print(Instance);
    }

    protected virtual void Init() { }
}