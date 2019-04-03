using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurfaceHead : SurfaceChild, ISueface
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

    protected override void Awake()
    {
        base.Awake();
        hpmaxw = hps.sizeDelta.x;
        mpmaxw = mps.sizeDelta.x;
    }

    void Start()
    {
        GameEvent.PlayerData.OnChange_HP += OnHpchange;
    }

    private void Ini(int hp,int mp,string name,int level)
    {
        hpt.text = hp.ToString();
        mpt.text = mp.ToString();
        playername.text = name;
        this.level.text = level.ToString();
    }

    private void OnHpchange(int min, int max)
    {
        if (max == 0)
        {
            Debugger.UI.LogError("血量最大值  不可能为0");
            return;
        }
        var v = hps.sizeDelta;
        v.x = min / max * hpmaxw;
        hps.sizeDelta = v;
        hpt.text = min.ToString();
    }

}
