﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

namespace FSM
{
    public class FSMMap
    {

    }

    //描述玩家  相机   敌人  物体的位置
    public class DesignMap : FSM_State
    {
        Transform player;
        GameMap map;
        CamaraFollow cf;
        Transform mapPa;
        public override void Initialization()
        {
            map = fSMController.variable[Define.FSMAI.MAP] as GameMap;
            cf = fSMController.variable[Define.FSMAI.CAMERA] as CamaraFollow;
            player = fSMController.variable[Define.FSMAI.TARGET] as Transform;
            mapPa = fSMController.variable[Define.FSMAI.MAPITEMPA] as Transform;
            StateName = Define.FSMAI.DESIGNMAP;
        }

        public override void Exit()
        {

        }

        public override void Start()
        {
            Vector2 v = map.CurrentSegment.wall;
            map.walls[0].SetPositonX(v.x);        //墙
            map.walls[1].SetPositonX(v.y);
            player.position = map.CurrentSegment.swpanPos;              //玩家
            cf.transform.SetPositonX(map.CurrentSegment.swpanPos.x);    //相机
            cf.LimitCaViewH(v);
        }

        public override void Update()
        {
            var a = map.CurrentSegment.placeData;
            for (int i = 0; i < a.Count; i++)
            {
                Swpan(a[i]);
            }
            fSMController.RunState(Define.FSMAI.MONITORMAP);
        }

        public void Swpan(PlaceData data)
        {
            var g = GameApp.pool.GetProp(Define.FSMAI.MAPITEM, data.spwanpos, data.spwanRo, mapPa);
            g.SetActive(true);
            Enimy2D a = g.GetComponent<Enimy2D>();
            a.mapdata = data;
            a.OnEnimyChange += A_OnEnimyChange;
            var b = new RoleData();
            b.hp = 100;
            b.moveSpeed = 1.5f;
            a.IniData(b, Resources.Load<RuntimeAnimatorController>(string.Format("Animator/{0}", data.mapObjid)));
        }

        private void A_OnEnimyChange(Enimy2D sender, Enimy2D.EnimyArg e)
        {
            if (e.isdie)
            {
                map.CurrentSegment.placeData.Remove(sender.mapdata);
                sender.OnEnimyChange -= A_OnEnimyChange;
            }
        }
    }

    //监视地图内的物体  打开或关闭门
    public class MonitorMap : FSM_State
    {
        MapSegment map;
        MapDoor[] doors;
        GameMap a;
        Transform mapPa;
        public override void Initialization()
        {
            a = fSMController.variable[Define.FSMAI.MAP] as GameMap;
            mapPa = fSMController.variable[Define.FSMAI.MAPITEMPA] as Transform;
            StateName = Define.FSMAI.MONITORMAP;
        }

        public override void Exit()
        {
            foreach (var item in doors)
            {
                if (item == null)
                {
                    break;
                }
                item.CloseDoor();
                GameApp.pool.Recycle(item.gameObject, Define.FSMAI.MAP);
            }
            for (int i = 0; i < mapPa.childCount; i++)
            {
                var g = mapPa.GetChild(i).gameObject;
                var item = g.GetComponent<IMapitem>();
                item.mapdata.spwanpos = mapPa.GetChild(i).position;
                item.mapdata.spwanRo = mapPa.GetChild(i).rotation;
                GameApp.pool.Recycle(g, Define.FSMAI.MAPITEM);
                g.SetActive(false);
            }
            doors = null;
        }

        public override void Start()
        {
            map = a.CurrentSegment;
            doors = new MapDoor[map.childs.Length];
            for (int i = 0; i < map.childs.Length; i++)
            {
                if (map.childs[i] == null)
                {
                    break;
                }
                int n = i;
                doors[n] = GameApp.pool.GetProp<MapDoor>(Define.FSMAI.MAP,
                    map.childs[i].swpanPos, Quaternion.identity, transform);
                doors[n].SetDoorData(map.childs[n]);
                doors[n].OnPlayerPass = (_) => a.SkipMap(map.childs[n].mapid);
            }
            OnMapItemchange();
        }

        private void OpenDoor()
        {
            foreach (var item in doors)
            {
                if (item == null)
                {
                    break;
                }
                item.OpenDoor();
            }
        }

        private void CloseDoor()
        {
            foreach (var item in doors)
            {
                if (item == null)
                {
                    break;
                }
                item.CloseDoor();
            }
        }

        public void OnMapItemchange()
        {
            var seg = map.placeData.Find(_ => _.mustBeDestroyed == true);
            if (seg == null)
            {
                OpenDoor();
            }
            else
            {
                CloseDoor();
            }
        }

        public override void Update()
        {

        }
    }

}