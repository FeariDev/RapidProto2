using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T privateInstance;

    public static T Instance
    {
        get
        {
            if (privateInstance == null)
            {
                Debug.LogError($"{typeof(T).BaseType} instance not set properly! Trying to find one.");

                privateInstance = FindFirstObjectByType<T>();

                if (privateInstance == null)
                {
                    Debug.LogError($"No instance of {typeof(T).BaseType} was found in the scene!");
                }
                else
                {
                    Debug.Log($"An instance of {typeof(T).BaseType} was found!");
                }
            }

            return privateInstance;
        }
    }



    protected virtual void Awake()
    {
        SetInstance();
    }

    protected virtual void OnDestroy()
    {
        if (privateInstance == this) privateInstance = null;
    }



    void SetInstance()
    {
        if (privateInstance == null) privateInstance = this as T;
        else
        {
            if (privateInstance == this as T) return;

            Debug.LogWarning($"Another instance of {typeof(T).BaseType} was found in GameObject '{gameObject.name}'! Destroying this instance.");
            Destroy(gameObject);
        }
    }
}
