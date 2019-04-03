using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionUI : SurfaceChild
{
    public Button close;
    public Transform pa;
    private Transform prefab;

    bool isStart;
    void Start()
    {
        if (isStart)
        {
            return;
        }
        close.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        prefab = pa.GetChild(0);
        prefab.gameObject.SetActive(false);
        isStart = true;

    }

    public override void Open()
    {
        base.Open();
        if (!isStart)
        {
            Start();
        }
    }

    public override void Close(bool destroy)
    {
        base.Close(destroy);
        if (destroy)
        {
            close.onClick.RemoveAllListeners();
        }
    }

}
