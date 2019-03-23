using System.Collections;
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
        public override void Initialization()
        {
            map = fSMController.variable[Define.FSMAI.MAP] as GameMap;
            cf = fSMController.variable[Define.FSMAI.CAMERA] as CamaraFollow;
            player = fSMController.variable[Define.FSMAI.TARGET] as Transform;
            StateName = Define.FSMAI.DESIGNMAP;
        }

        public override void Exit()
        {

        }

        public override void Start()
        {

        }

        public override void Update()
        {
            Vector2 v = map.CurrentSegment.wall;
            map.walls[0].SetPositonX(v.x);        //墙
            map.walls[1].SetPositonX(v.y);
            player.position = map.CurrentSegment.swpanPos;              //玩家
            cf.transform.SetPositonX(map.CurrentSegment.swpanPos.x);    //相机
            cf.LimitCaView(v);
            fSMController.RunState(Define.FSMAI.MONITORMAP);
        }
    }

    //监视地图内的物体  打开或关闭门
    public class MonitorMap : FSM_State
    {
        MapSegment map;
        MapDoor[] doors;
        GameMap a;
        public override void Initialization()
        {
            a = fSMController.variable[Define.FSMAI.MAP] as GameMap;
            StateName = Define.FSMAI.MONITORMAP;
        }

        public override void Exit()
        {
            foreach (var item in doors)
            {
                if (item==null)
                {
                    break;
                }
                item.CloseDoor();
                GameApp.pool.Recycle(item.gameObject,Define.FSMAI.MAP);
            }
            doors = null;
        }

        public override void Start()
        {
            map = a.CurrentSegment;
            doors = new MapDoor[map.childs.Length];
            for (int i = 0; i < map.childs.Length; i++)
            {
                if (map.childs[i]==null)
                {
                    break;
                }
                doors[i] = GameApp.pool.GetProp<MapDoor>(Define.FSMAI.MAP,
                    map.childs[i].swpanPos, Quaternion.identity, transform);
                doors[i].SetDoorData(map.childs[i]);
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