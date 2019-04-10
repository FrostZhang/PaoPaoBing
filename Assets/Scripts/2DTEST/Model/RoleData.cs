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

    private List<PlotData> plotDatas1;   //人物对话的资料
    public List<PlotData> PlotDatas1
    {
        get
        {
            if (plotDatas1 == null)
            {
                CSVLoader plot1 = new CSVLoader();
                try
                {
                    plot1.ReadUTF8File(Application.streamingAssetsPath + "/Plot.csv");
                    int r = plot1.GetRows();
                    int c = plot1.GetCols();
                    plotDatas1 = new List<PlotData>();
                    for (int i = 1; i < r; i++)
                    {
                        var a = new PlotData();
                        a.plotID = int.Parse(plot1.GetValueAt(i, 0));
                        a.btns = int.Parse(plot1.GetValueAt(i, 1));
                        a.okOrSkip = int.Parse(plot1.GetValueAt(i, 2));
                        int refuse;
                        int.TryParse(plot1.GetValueAt(i, 3), out refuse);
                        a.refuse = refuse;
                        a.chatstr = string.Format(plot1.GetValueAt(i, 4), "An");
                        plotDatas1.Add(a);
                    }
                }
                catch (System.Exception e)
                {
                    Debugger.App.LogError(e);
                    return null;
                }
                return plotDatas1;
            }
            else
            {
                return plotDatas1;
            }
        }
    } 

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


public class PlotData
{
    public int plotID;  //对话ID
    public string chatstr;  //内容
    public int btns;    //拥有选项

    public int okOrSkip;
    public int refuse;
}