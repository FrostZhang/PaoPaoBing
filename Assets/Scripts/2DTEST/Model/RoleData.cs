using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleData
{
    public int gameid;          //物体id
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
    public string account;      //账号
    public string passworld;    //密码
    public int roles;           //角色总数量

    //当前角色
    public int roleid;          //角色序列号
    public RoleData basedata;   //基本资料
    public List<BagItem> bagitems;  //背包物品

    public CustomData()
    {
        //test
        basedata = new RoleData() { hp = 100, atk = 2, def = 3, fightSpeed = 1 };
    }
}

public class BagItem
{
    public int bagitemType;  //物品类型
    public int restrictionlevel;  //限制使用等级
    public int superNumber;   //堆叠数量

    public string itemID;    //物品ID （对应图标路径）

}

public class MissionData
{
    public int missionID;   //任务编号
    public int level;   //接受等级

    public int missionType; //任务类型 0对话 1击杀
    public string missonTip; //任务简述
    public int mapid;        //地图位置


}