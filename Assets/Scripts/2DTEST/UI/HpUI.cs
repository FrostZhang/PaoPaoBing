using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    public string rolename { get { return nametext.text; } }
    public SuperTextMesh nametext;
    public Slider hpslider;
    public Transform target;
    public bool IsShow
    {
        get { return cg.alpha == 1; }
        set
        {
            if (value)
            {
                cg.alpha = 1;
            }
            else
            {
                cg.alpha = 0;
            }
        }
    }

    public bool isoutcamera;

    [SerializeField]
    private CanvasGroup cg;

    private Transform tr;

    void Awake()
    {
        tr = transform;
    }

    public void UpHp(int value, int max = 0, int min = 0)
    {
        if (max != 0 && max > min)
        {
            hpslider.maxValue = max;
        }
        hpslider.value = value;
    }

    public void Uppos()
    {
        tr.position = target.position;
    }
}
