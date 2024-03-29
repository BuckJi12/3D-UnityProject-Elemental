using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingleTon<PoolManager>
{
    private Dictionary<string, Stack<GameObject>> poolDic;

    [SerializeField]
    private List<Poolable> poolPrefab;

    private void Awake()
    {
        poolDic = new Dictionary<string, Stack<GameObject>>();
    }

    private void Start()
    {
        CreatePool();
    }
    public void CreatePool()
    {
        for (int i = 0; i < poolPrefab.Count; i++)
        {
            Stack<GameObject> stack = new Stack<GameObject>();
            for (int j = 0; j < poolPrefab[i].count; j++)
            {
                GameObject instance = Instantiate(poolPrefab[i].prefab);
                instance.SetActive(false);
                instance.gameObject.name = poolPrefab[i].prefab.name;
                instance.transform.SetParent(poolPrefab[i].container);
                stack.Push(instance);
            }
            poolDic.Add(poolPrefab[i].prefab.name, stack);
        }
    }

    public GameObject Get(GameObject prefab)
    {
        Stack<GameObject> stack = poolDic[prefab.name];
        if (stack.Count > 0)
        {
            GameObject instance = stack.Pop();
            instance.SetActive(true);
            instance.transform.SetParent(null);
            return instance;
        }

        Poolable poolable = poolPrefab.Find((x) => prefab.name == x.prefab.name);
        if (poolable.enableCreation)
        {
            GameObject instance = Instantiate(prefab);
            instance.SetActive(true);
            instance.gameObject.name = prefab.name;
            instance.transform.parent = null;
            return instance;
        }

        else { return null; }
    }

    public void Release(GameObject instance)
    {
        Poolable poolable = poolPrefab.Find((x) => instance.name == x.prefab.name);
        instance.transform.SetParent(poolable.container);

        Stack<GameObject> stack = poolDic[instance.name];
        instance.SetActive(false);
        stack.Push(instance);
    }

    public void EnemyRespawn(Enemy enemy, float delayTime)
    {
        enemy.gameObject.SetActive(false);
        StartCoroutine(DelayTime(enemy, delayTime));
    }

    private IEnumerator DelayTime(Enemy enemy, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        enemy.gameObject.SetActive(true);
    }

    [Serializable]
    public struct Poolable
    {
        public GameObject prefab;
        public int count;
        public Transform container;
        public bool enableCreation;
    }
}
