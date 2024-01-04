using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDict; 
    
    private void Awake()
    {
        poolDict = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++) {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDict.Add(pool.tag, objectPool);
        }
    }
    
    public GameObject SpawnFromPool(string tag) {
        if (!poolDict.ContainsKey(tag))
            return null;

        GameObject obj = poolDict[tag].Dequeue();
        poolDict[tag].Enqueue(obj);

        return obj;
    }

    public void InsertToPool(Pool pool)
    {
        if (poolDict.ContainsKey(pool.tag))
            return;
        
        Queue<GameObject> objectPool = new Queue<GameObject>();
        for (int i = 0; i < pool.size; i++) {
            GameObject obj = Instantiate(pool.prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
        poolDict.Add(pool.tag, objectPool); 
    }
    
    public void DeleteToPool(string tag)
    {
        poolDict.Remove(tag);
    }
}
