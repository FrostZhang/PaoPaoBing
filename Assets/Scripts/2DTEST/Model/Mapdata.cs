using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapData
    {
        public int mapid;  //世界地图块id
        List<MapSegment> segments;

        public MapData()
        {
            segments = new List<MapSegment>();
        }

        /// <summary>
        /// 创建地图块
        /// </summary>
        /// <returns></returns>
        public MapSegment CreatSegment()
        {
            var a = new MapSegment(segments.Count);
            segments.Add(a);
            return a;
        }

        public MapSegment GetSegment(int id)
        {
            if (id >= segments.Count)
            {
                return null;
            }
            else
            {
                return segments[id];
            }
        }
    }

    public class MapSegment
    {
        public int mapid { get; private set; }   //地图块id
        public MapChild[] childs;    //可通往
        public Vector2 wall;  //左右墙限制

        public Vector3 swpanPos { get; set; }   //出生点

        public MapSegment(int mapid)
        {
            this.mapid = mapid;
            placeData = new List<PlaceData>();
            childs = new MapChild[4];
        }

        //敌人及物体信息
        public List<PlaceData> placeData;

        public MapChild GetChild(int mapid)
        {
            foreach (var item in childs)
            {
                if (item == null)
                {
                    break;
                }
                if (item.mapid == mapid)
                {
                    return item;
                }
            }
            return null;
        }
    }

    public class PlaceData
    {
        public int mapObjid;            //物体id
        public Vector3 spwanpos;        //出生地
        public bool mustBeDestroyed;    //标志物体必须被摧毁，否则无法达到下一关
    }

    public class MapChild
    {
        public int mapid = -1;  //下张地图块的id
        public Vector3 swpanPos;  //连接点

        public int turn;

        /// <summary>
        /// ┏┳┳┳┳┳┓
        /// ┣  0  ┫
        /// ┣2   3┫
        /// ┣  1  ┫
        /// ┗┻┻┻┻┻┛
        /// 0朝下  1朝上  2朝右   3朝左
        /// </summary>
        public MapChild(int mapid, Vector3 swpanPlace,int turn)
        {
            this.mapid = mapid;
            this.swpanPos = swpanPlace;
            this.turn = turn;
        }
    }
}
