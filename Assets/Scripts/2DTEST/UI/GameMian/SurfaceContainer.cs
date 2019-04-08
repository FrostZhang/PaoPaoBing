using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceContainer : MonoBehaviour
{
    [HideInInspector] public Transform tr;
    [HideInInspector] public GameObject go;

    public List<SurfaceChild> suefaces;

    private void Awake()
    {
        tr = transform;
        go = gameObject;
        suefaces = new List<SurfaceChild>();
    }

    public bool Contains<T>() where T : SurfaceChild
    {
        foreach (var item in suefaces)
        {
            if (item is T)
            {
                return true;
            }
        }
        return false;   
    }

    public bool TryGet<T>(out T value) where T : SurfaceChild
    {
        foreach (var item in suefaces)
        {
            if (item is T)
            {
                value = (T)item;
                return true;
            }
        }
        value = default(T);
        return false;
    }

    public T Open<T>() where T : SurfaceChild
    {
        foreach (var item in suefaces)
        {
            if (item is T)
            {
                item.Open();
                return (T)item;
            }
        }
        T t = Resources.Load<T>("Surface/" + typeof(T).ToString());
        if (t)
        {
            var v = Instantiate(t, tr);
            suefaces.Add(v);
            return v;
        }
        else
        {
            Debugger.Resource.LogError("没有这个资源 " + typeof(T).ToString());
            return null;
        }
    }

    public void Close<T>(bool destory = false)
    {
        for (int i = 0; i < suefaces.Count; i++)
        {
            if (suefaces[i] is T)
            {
                if (destory)
                {
                    Destroy(suefaces[i].go);
                    suefaces.Remove(suefaces[i]);
                }
                else
                    suefaces[i].Close();
                return;
            }
        }
    }

    public void CloseAll(bool destory = false)
    {
        if (destory)
        {
            for (int i = 0; i < suefaces.Count; i++)
            {
                if (!suefaces[i].dontDestroyOnLoad)
                {
                    Destroy(suefaces[i].go);
                    suefaces.Remove(suefaces[i]);
                }
                else
                {
                    suefaces[i].Close();
                }
            }
        }
        else
        {
            for (int i = 0; i < suefaces.Count; i++)
            {
                suefaces[i].Close();
            }
        }

    }

    public void OpenAll()
    {
        foreach (var item in suefaces)
        {
            item.Open();
        }
    }
}