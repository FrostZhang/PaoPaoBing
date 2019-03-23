using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public static class Extension_Method
{

    public static void SetPositonX(this Transform tr, float x)
    {
        Vector3 v = tr.position;
        v.x = x;
        tr.position = v;
    }

    public static void SetPositonY(this Transform tr, float y)
    {
        Vector3 v = tr.position;
        v.y = y;
        tr.position = v;
    }
    public static void SetPositonZ(this Transform tr, float z)
    {
        Vector3 v = tr.position;
        v.z = z;
        tr.position = v;
    }
}
