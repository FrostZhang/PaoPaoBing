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
        foreach (var item in suefaces)
        {
            if (item is T)
            {
                if (destory)
                {
                    suefaces.Remove(item);
                }
                item.Close(destory);
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
                if (!suefaces[i].ResidMemory)
                {
                    suefaces[i].Close(destory);
                    suefaces.Remove(suefaces[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < suefaces.Count; i++)
            {
                if (!suefaces[i].ResidMemory)
                {
                    suefaces[i].Close(destory);
                }
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