using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//基本角色
public class Role : MonoBehaviour
{
    [SerializeField]
    protected bool hasHpText;
    public Transform hptextPos;
    protected HpUI hpText;
    public RoleData data;

    protected void BuildHub()
    {
        if (hasHpText && !hpText)
        {
            if (!hptextPos)
            {
                hptextPos = transform;
            }
            hpText = GameEvent.HPText.InstansHpUI(hptextPos, data.name); 
            hpText.UpHp(data.hp, data.hp, 0);
        }
    }

    protected void ShowHP(bool isshow)
    {
        hpText.IsShow = isshow;
        hpText.isoutcamera = isshow;
        if (isshow)
        {
            hpText.Uppos();
        }
    }

    protected void ShowHpChange(int hurt, bool isminus = true)
    {
        if (isminus)
        {
            GameEvent.HPText.ShowDYUI(hptextPos.position, string.Concat("- ", hurt.ToString()));
        }
        else
        {
            GameEvent.HPText.ShowDYUI(hptextPos.position, string.Concat("+ ", hurt.ToString()));
        }
        if (hpText)
        {
            hpText.UpHp(data.hp);
            ShowHP(true);
        }
    }

    protected virtual void OnDestroy()
    {
        if (hasHpText && hpText)
        {
            GameEvent.HPText.RecycleUI(hpText);
        }
    }
}