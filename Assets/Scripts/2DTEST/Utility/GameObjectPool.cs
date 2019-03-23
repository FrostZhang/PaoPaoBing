using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool
{

    public SortedDictionary<string, Stack<GameObject>> pools;

    public GameObjectPool() { pools = new SortedDictionary<string, Stack<GameObject>>(); }
    public GameObjectPool(GameObject[] prefbs)
    {
        pools = new SortedDictionary<string, Stack<GameObject>>();
        foreach (var item in prefbs)
        {
            SetPrefab(item);
        }
    }

    public T GetProp<T>(string name, Vector3 position, Quaternion rotation, Transform parent) where T : MonoBehaviour
    {
        if (pools.ContainsKey(name))
        {
            if (pools[name].Count > 1)
            {
                var a = pools[name].Pop();
                a.transform.SetParent(parent);
                a.transform.SetPositionAndRotation(position, rotation);
                return a.GetComponent<T>();
            }
            else
            {
                return Object.Instantiate<T>(pools[name].Peek().GetComponent<T>(), position, rotation, parent);
            }
        }
        return default(T);
    }

    public GameObject GetProp(string name, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (pools.ContainsKey(name))
        {
            if (pools[name].Count > 1)
            {
                var a = pools[name].Pop();
                a.transform.SetParent(parent);
                a.transform.position = position;
                a.transform.SetPositionAndRotation(position, rotation);
                return a;
            }
            else
            {
                var obj = Object.Instantiate(pools[name].Peek(), position, rotation, parent);
                obj.name = name;
                return obj;
            }
        }
        return null;
    }

    public void Recycle(GameObject obj, string name = null)
    {
        if (name == null)
        {
            name = obj.name;
        }
        if (pools.ContainsKey(name))
        {
            pools[name].Push(obj);
        }
    }

    public void SetPrefab(GameObject prefab, string name = null)
    {
        name = (name == null) ? prefab.name : name;
        if (pools.ContainsKey(name))
        {
            //覆盖
            while (pools[name].Count > 0)
            {
                Object.Destroy(pools[name].Pop());
            }
            pools.Remove(name);
            Debug.Log("对象池已有同名目标,已将其覆盖");
        }
        var _ = new Stack<GameObject>();
        _.Push(prefab);
        pools.Add(name, _);
    }

    public void Clear()
    {
        foreach (var item in pools.Values)
        {
            while (item.Count > 1)
            {
                Object.Destroy(item.Pop());
            }
        }
    }

}

public class ClassPool<T> where T : class, new()
{
    Stack<T> poolClass;

    public ClassPool()
    {
        poolClass = new Stack<T>();
    }

    public T Get()
    {
        if (poolClass.Count > 0)
        {
            return poolClass.Pop();
        }
        return new T();
    }

    public void Recycle(T t)
    {
        poolClass.Push(t);
    }

    public void Clear()
    {
        poolClass.Clear();
    }
}