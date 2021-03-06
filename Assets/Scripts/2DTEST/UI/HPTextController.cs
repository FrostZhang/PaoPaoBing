﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HPTextController : SurfaceChild
{
    const string HPText = "HPText";
    const string DyHpText = "DyHpText";

    protected override void Awake()
    {
        base.Awake();
        GameEvent.HPText.InstansHpUI += InstansHpText;
        GameEvent.HPText.RecycleUI += RecycleHpUi;
        GameEvent.HPText.ShowDYUI += ShowDyhp;
    }

    public List<HpUI> hpUIs;
    public HpUI InstansHpText(Transform pos, string name)
    {
        GameObject ob = GameApp.pool.GetProp(HPText, pos.position, Quaternion.identity, tr);
        var hpui = ob.GetComponent<HpUI>();
        hpui.nametext.text = name == null ? string.Empty : name;
        hpui.target = pos;
        hpui.Uppos();
        hpui.IsShow = false;
        hpUIs.Add(hpui);
        return hpui;
    }

    public void ShowDyhp(Vector3 pos, string text)
    {
        GameObject dy = GameApp.pool.GetProp(DyHpText, pos, Quaternion.identity, tr);
        var st = dy.GetComponent<SuperTextMesh>();
        st.text = text;
        st.color = Color.red;
        dy.transform.DOMoveY(2, 1f).SetRelative();
        DOTween.ToAlpha(() => st.color, (_) => { st.color = _; st.Rebuild(); }, 0, 1)
            .SetEase(Ease.InOutFlash)
            .OnComplete(() =>
        {
            GameApp.pool.Recycle(dy, DyHpText);
        });
    }

    public void RecycleHpUi(HpUI hpui)
    {
        hpui.IsShow = false;
        GameApp.pool?.Recycle(hpui.gameObject, HPText);
        hpUIs.Remove(hpui);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < hpUIs.Count; i++)
        {
            if (hpUIs[i].IsShow)
            {
                if (IsOutCamera(hpUIs[i].target.position))
                {
                    hpUIs[i].IsShow = false;
                    hpUIs[i].isoutcamera = true;
                }
                else
                {
                    hpUIs[i].Uppos();
                }
            }
            else if (hpUIs[i].isoutcamera && !IsOutCamera(hpUIs[i].target.position))
            {
                hpUIs[i].IsShow = true;
                hpUIs[i].isoutcamera = false;
                hpUIs[i].Uppos();
            }
        }
    }

    private bool IsOutCamera(Vector3 pos)
    {
        pos = Camera.main.WorldToViewportPoint(pos);
        if (pos.x < 0 || pos.x > 1/* || pos.y < 0 || pos.y > 1*/)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Clear()
    {
        DOTween.CompleteAll();
        for (int i = 0; i < hpUIs.Count; i++)
        {
            RecycleHpUi(hpUIs[i]);
        }
    }
}
