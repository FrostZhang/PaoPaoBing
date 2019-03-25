using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using FSM;

public class GameMap : MonoBehaviour
{
    public Transform[] walls;
    [SerializeField] protected GameObject doorPrefab;
    [SerializeField] protected GameObject enimyPrefab;

    //public GameObject enimy;
    private Transform tr;
    MapData mapData;

    public MapSegment CurrentSegment;
    FSMController fsm;

    private Transform mapItemPa;

    void Start()
    {
        tr = gameObject.transform;
        mapItemPa = new GameObject(Define.FSMAI.MAPITEMPA).transform;

        LondMapData();

        CurrentSegment = mapData.GetSegment(0);
        GameApp.pool.SetPrefab(doorPrefab, Define.FSMAI.MAP);
        GameApp.pool.SetPrefab(enimyPrefab, Define.FSMAI.MAPITEM);

        fsm = new FSMController(gameObject, tr);
        fsm.variable.Add(Define.FSMAI.CAMERA, GameApp.cameraCt);
        fsm.variable.Add(Define.FSMAI.TARGET, GameApp.Instance.player2D.tr);
        fsm.variable.Add(Define.FSMAI.MAP, this);
        fsm.variable.Add(Define.FSMAI.MAPITEMPA, mapItemPa);
        DesignMap designMap = new DesignMap();
        fsm.AddState(designMap);
        MonitorMap monitorMap = new MonitorMap();
        fsm.AddState(monitorMap);
        fsm.Initialization();
    }

    private void Update()
    {
        fsm.UpdateState();
    }

    //进入下一个房间
    public void SkipMap(int mapid)
    {
        MapSegment seg = mapData.GetSegment(mapid);
        var a = seg.GetChild(CurrentSegment.mapid);
        if (a != null)
        {
            switch (a.turn)
            {
                case 0:
                    seg.swpanPos = a.swpanPos + Vector3.back * 2;
                    break;
                case 1:
                    seg.swpanPos = a.swpanPos + Vector3.forward * 2;
                    break;
                case 2:
                    seg.swpanPos = a.swpanPos + Vector3.right * 2;
                    break;
                case 3:
                    seg.swpanPos = a.swpanPos + Vector3.left * 2;
                    break;
                default:
                    break;
            }
            CurrentSegment = seg;
            fsm.RunState(Define.FSMAI.DESIGNMAP);
        }
    }


    public void LondMapData()
    {
        mapData = new MapData();
        var seg = mapData.CreatSegment();
        seg.swpanPos = new Vector3(-5.78f, 0, 0);
        seg.wall = new Vector2(-8.4f, 10);
        seg.childs[0] = new MapChild(1, new Vector3(6.35f, 0, 4.79f), 0);
        seg.childs[1] = new MapChild(2, new Vector3(9.5f, 0, 0), 3);
        seg.placeData = new List<PlaceData>();
        seg.placeData.Add(new PlaceData() { mapObjid = 3, mustBeDestroyed = false, spwanpos = new Vector3(2.73f, 0, 0), spwanRo = Quaternion.identity });
        seg.placeData.Add(new PlaceData() { mapObjid = 3, mustBeDestroyed = false, spwanpos = new Vector3(2.73f, 0, 0), spwanRo = Quaternion.identity });
        seg.placeData.Add(new PlaceData() { mapObjid = 3, mustBeDestroyed = false, spwanpos = new Vector3(2.73f, 0, 0), spwanRo = Quaternion.identity });

        var seg1 = mapData.CreatSegment();
        seg1.wall = new Vector2(10, 28.4f);
        seg1.childs[0] = new MapChild(0, new Vector3(14.73f, 0, -1.35f), 1);
        seg1.placeData.Add(new PlaceData() { mapObjid = 5, mustBeDestroyed = false, spwanpos = new Vector3(23.52f, 0, 0), spwanRo = Quaternion.identity });
        seg1.placeData.Add(new PlaceData() { mapObjid = 5, mustBeDestroyed = false, spwanpos = new Vector3(23.52f, 0, 0), spwanRo = Quaternion.identity });
        seg1.placeData.Add(new PlaceData() { mapObjid = 5, mustBeDestroyed = false, spwanpos = new Vector3(23.52f, 0, 0), spwanRo = Quaternion.identity });

        var seg2 = mapData.CreatSegment();
        seg2.wall = new Vector2(10, 28.4f);
        seg2.childs[0] = new MapChild(0, new Vector3(10.5f, 0, 0), 2);
        seg2.childs[1] = new MapChild(3, new Vector3(28, 0, 0), 3);
        seg2.placeData.Add(new PlaceData() { mapObjid = 6, mustBeDestroyed = false, spwanpos = new Vector3(23.52f, 0, 0), spwanRo = Quaternion.identity });
        seg2.placeData.Add(new PlaceData() { mapObjid = 6, mustBeDestroyed = false, spwanpos = new Vector3(23.52f, 0, 0), spwanRo = Quaternion.identity });
        seg2.placeData.Add(new PlaceData() { mapObjid = 6, mustBeDestroyed = false, spwanpos = new Vector3(23.52f, 0, 0), spwanRo = Quaternion.identity });

        var seg3 = mapData.CreatSegment();
        seg3.wall = new Vector2(28.4f, 45.4f);
        seg3.childs[0] = new MapChild(2, new Vector3(29, 0, 0), 2);
        seg3.childs[1] = new MapChild(4, new Vector3(35f, 0, -1.35f), 1);
        seg3.placeData.Add(new PlaceData() { mapObjid = 8, mustBeDestroyed = false, spwanpos = new Vector3(40f, 0, 0), spwanRo = Quaternion.identity });
        seg3.placeData.Add(new PlaceData() { mapObjid = 8, mustBeDestroyed = false, spwanpos = new Vector3(40f, 0, 0), spwanRo = Quaternion.identity });
        seg3.placeData.Add(new PlaceData() { mapObjid = 8, mustBeDestroyed = false, spwanpos = new Vector3(40f, 0, 0), spwanRo = Quaternion.identity });


        var seg4 = mapData.CreatSegment();
        seg4.wall = new Vector2(-8.4f, 10);
        seg4.childs[0] = new MapChild(3, new Vector3(6.35f, 0, 4.79f), 0);
        seg4.placeData.Add(new PlaceData() { mapObjid = 6, mustBeDestroyed = false, spwanpos = new Vector3(2.73f, 0, 0), spwanRo = Quaternion.identity });
        seg4.placeData.Add(new PlaceData() { mapObjid = 6, mustBeDestroyed = false, spwanpos = new Vector3(2.73f, 0, 0), spwanRo = Quaternion.identity });
        seg4.placeData.Add(new PlaceData() { mapObjid = 6, mustBeDestroyed = false, spwanpos = new Vector3(2.73f, 0, 0), spwanRo = Quaternion.identity });

    }
}