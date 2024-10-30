using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    #region WRAPPING
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }
    public T[] LoadAll<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>(path);
    }
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        return Instantiate(prefab, parent);
    }
    public GameObject Instantiate(PrefabType type, Transform parent = null)
    {
        int index = (int)type;

        if (prefabs[index] == null)
        {
            Debug.Log($"Failed to load prefab : {prefabs[index]}");
            return null;
        }

        return Instantiate(prefabs[index], parent) as GameObject;
    }
    public void Destroy(GameObject go, float time = 0)
    {
        if (go == null)
        {
            Debug.Log($"Failed to destroy object : object is null");
            return;
        }
        Destroy(go, time);
    }
    #endregion

    private Object[] prefabs;

    public override void Awake()
    {
        base.Awake();

        prefabs = LoadAll<GameObject>("Prefabs");
    }
}
