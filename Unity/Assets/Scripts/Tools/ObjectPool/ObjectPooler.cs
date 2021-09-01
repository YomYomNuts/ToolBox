using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region SINGLETON
    static ObjectPooler instance;

    public static ObjectPooler Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<ObjectPooler>();
            if (instance == null)
                instance = new GameObject("ObjectPooler").AddComponent<ObjectPooler>();
            return instance;
        }
    }
    #endregion

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int numberObjects;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionnary;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        poolDictionnary = new Dictionary<string, Queue<GameObject>>();

        for (int i = 0; i < pools.Count; ++i)
        {
            Pool pool = pools[i];
            Queue<GameObject> objectPool = new Queue<GameObject>();
            Transform parent = new GameObject(pool.tag).transform;
            parent.parent = this.transform;

            for (int j = 0; j < pool.numberObjects; ++j)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.parent = parent;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionnary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3? position = null, Quaternion? rotation = null)
    {
        if (!poolDictionnary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionnary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position != null ? position.Value : Vector3.zero;
        objectToSpawn.transform.rotation = rotation != null ? rotation.Value : Quaternion.identity;

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        if (pooledObject != null)
            pooledObject.OnObjectSpawn();

        poolDictionnary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
