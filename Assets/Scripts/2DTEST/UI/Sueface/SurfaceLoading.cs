using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceLoading : SurfaceChild {

    public RectTransform bg;
    public RectTransform fill;

    void Start ()
    {
        var a = fill.sizeDelta;
        a.x = 0;
        fill.sizeDelta = a;

    }

    public void OnSceneLoading(float f)
    {
        var a = fill.sizeDelta;
        a.x = bg.sizeDelta.x * f;
        fill.sizeDelta = a;
    }
}
