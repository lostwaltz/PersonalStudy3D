using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Pool;
using static ObjectPool;


public class ObjectPool : Singleton<ObjectPool>
{
    [System.Serializable]
    public class InitData
    {
        public PooledObject prefab;
        public int capacity;
        public int maxSize;
    }

    public List<InitData> poolPrefabList;
    private Dictionary<string, IObjectPool<PooledObject>> poolDictionary = new Dictionary<string, IObjectPool<PooledObject>>();

    public override void Awake()
    {
        base.Awake();

        InitObjectPool();
    }

    private void InitObjectPool()
    {
        for (int i = 0; i < poolPrefabList.Count; i++)
        {
            CreateNewPool(poolPrefabList[i]);

            InitData pool = poolPrefabList[i];
            string key = poolPrefabList[i].prefab.name;
            for (int j = 0; j < pool.capacity; j++)
            {
                poolDictionary[key].Release(Instantiate(pool.prefab));
            }
        }
    }

    private void CreateNewPool(InitData pool)
    {
        poolDictionary[pool.prefab.name] = new ObjectPool<PooledObject>(() => OnCreatePool(pool.prefab), OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, true, pool.capacity, pool.maxSize);
    }


    private PooledObject OnCreatePool(PooledObject pooledObject)
    {
        return Instantiate(pooledObject);
    }

    private void OnGetFromPool(PooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(PooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(PooledObject pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    public PooledObject GetPooledObject(string key)
    {
        return poolDictionary[key].Get();
    }

    public void ReleaseObject(string key, PooledObject obj)
    {
        poolDictionary[key].Release(obj);
    }
}
