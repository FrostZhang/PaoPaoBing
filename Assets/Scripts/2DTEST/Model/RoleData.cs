using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleData
{
    public int gameid;          //全局唯一id
    public string name;         //名称
    public int hp;              //血
    public int def;             //物理防御
    public int atk;             //物理攻击
    public float moveSpeed;     //移动速度
    public float fightSpeed;    //攻击速度
}

public class EnimyData : RoleData
{
    public int animatorID;          //动画数据索引
}

public class CustomData
{
    public string account;  //账号
    public string passworld;  //密码
    public RoleData basedata;
}