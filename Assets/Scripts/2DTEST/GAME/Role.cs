using Sirenix.OdinInspector;
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

    protected virtual void Start()
    {
        if (hasHpText)
        {
            if (!hptextPos)
            {
                hptextPos = transform;
            }
            hpText = HPTextController.Instance.InstansHpText(hptextPos, data.name);
            hpText.UpHp(data.hp, data.hp, 0);
        }
    }

    private void ShowHP()
    {
        hpText.IsShow = true;
    }

    protected void ShowHpChange(int hurt, bool isminus = true)
    {
        if (isminus)
        {
            HPTextController.Instance.ShowDyhp(hptextPos.position, string.Concat("- ", hurt.ToString()));
        }
        else
        {
            HPTextController.Instance.ShowDyhp(hptextPos.position, string.Concat("+ ", hurt.ToString()));
        }
        if (hpText)
        {
            hpText.UpHp(data.hp);
            ShowHP();
        }
    }

    protected virtual void OnDestroy()
    {
        if (hasHpText)
        {
            HPTextController.Instance?.RecycleHpUi(hpText);
        }
    }
}