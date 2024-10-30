using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T s_Instance;
    public static T Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = (T)FindFirstObjectByType(typeof(T));

                if (s_Instance == null)
                {
                    SetupInstance();
                }
                else
                {
                    Debug.Log("[Singleton] " + (typeof(T).Name) + 
                        " instance already created: " + s_Instance.gameObject.name);
                }
            }

            return s_Instance;
        }
    }

    public virtual void Awake()
    {
        RemoveDuplicates();
    }

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += SceneManager_SceneUnloaded;
    }

    private void OnDisable()
    {
        if (s_Instance == this as T)
        {
            SceneManager.sceneUnloaded -= SceneManager_SceneUnloaded;
        }
    }

    private static void SetupInstance()
    {
        s_Instance = (T)FindFirstObjectByType(typeof(T));

        if (s_Instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = typeof(T).Name;

            s_Instance = gameObj.AddComponent<T>();
            DontDestroyOnLoad(gameObj);
        }
    }

    public void RemoveDuplicates()
    {
        if (s_Instance == null)
        {
            s_Instance = this as T;

            DontDestroyOnLoad(gameObject);
        }
        else if (s_Instance != this)
        {
            Debug.Log($"{ToString()} is Duplicate! ");
            Destroy(gameObject);
        }

    }

    protected virtual void SceneManager_SceneUnloaded(Scene scene)
    {
        if (s_Instance != null)
            Destroy(s_Instance.gameObject);

        s_Instance = null;
    }
}