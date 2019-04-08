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

    protected override void Awake()
    {
        base.Awake();
        prefab = pa.GetChild(0);
        prefab.gameObject.SetActive(false);
    }

    void Start()
    {
        close.onClick.AddListener(() =>
        {
            Close();
        });

        Transform t= Instantiate(prefab, pa);
        t.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            t.GetComponentInChildren<Button>().onClick.RemoveAllListeners();

        });
    }

    public override void Open()
    {
        base.Open();
    }

    private void ReadMissions()
    {

    }

    private void OnDestroy()
    {
        close.onClick.RemoveAllListeners();
    }

}
