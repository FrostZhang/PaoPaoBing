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

    public List<BagItem> bagitems;  //背包物品
}

public class BagItem
{
    public int bagitemType;  //物品类型
    public int restrictionlevel;  //限制使用等级
    public int superNumber;   //堆叠数量

    public string bagitemID;    //物品ID （对应图标路径）

}