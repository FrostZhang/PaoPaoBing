using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer
{
    public GameTimer()
    {
        eves = new List<Eve>();
        eves2 = new List<Eve>();
        evepool = new ClassPool<Eve>();
    }
    public class Eve
    {
        public Action act;
        public float time;
    }

    List<Eve> eves;
    List<Eve> eves2;

    ClassPool<Eve> evepool;
    public void Delay(Action act, float t, bool actualTime = false)
    {
        Eve e = evepool.Get();
        e.act = act;
        e.time = t;
        if (actualTime)
        {
            eves2.Add(e);
        }
        else
        {
            eves.Add(e);
        }
    }

    public void Update()
    {
        for (int n = 0; n < eves.Count; n++)
        {
            if ((eves[n].time -= Time.deltaTime) < 0)
            {
                eves[n].act.Invoke();
                evepool.Recycle(eves[n]);
                eves.Remove(eves[n]);
            }
        }
        for (int i = 0; i < eves2.Count; i++)
        {
            if ((eves2[i].time -= Time.unscaledDeltaTime) < 0)
            {
                eves2[i].act.Invoke();
                evepool.Recycle(eves2[i]);
                eves2.Remove(eves2[i]);
            }
        }
    }
}
