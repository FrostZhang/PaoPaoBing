using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurfaceHead: MonoBehaviour,ISueface
{
    public SuperTextMesh playername;
    public SuperTextMesh level;
    public SuperTextMesh hpt;
    public SuperTextMesh mpt;
    public RectTransform hps;
    public RectTransform mps;
    public Image headimg;

    private float hpmaxw;
    private float mpmaxw;

    private void Awake()
    {
        hpmaxw = hps.sizeDelta.x;
        mpmaxw = mps.sizeDelta.x;
    }

    // Use this for initialization
    void Start ()
    {
        GameEvent.PlayerData.OnChange_HP += OnHpchange;
	}

    private void OnHpchange(int min, int max)
    {
        if (max==0)
        {
            Debugger.UI.LogError("血量最大值  不可能为0");
            return;
        }
        var v = hps.sizeDelta;
        v.x = min / max * hpmaxw;
        hps.sizeDelta = v;
        hpt.text = min.ToString();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
