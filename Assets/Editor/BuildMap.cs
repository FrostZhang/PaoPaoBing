using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using Map;

public class BuildMap : MonoBehaviour
{

}

public class BuildMapWindow : EditorWindow
{
    public static BuildMapWindow buildMap;
    [MenuItem("Tools/BuildMap")]
    public static void Open()
    {
        buildMap = GetWindow<BuildMapWindow>("创建地图", false);
    }

    public MapData map;
    public List<MapData> datas = new List<MapData>();
    ReorderableList segList;

    private void OnEnable()
    {
        map = new MapData();
        map.segments = new List<MapSegment>();
        segList = new ReorderableList(map.segments, typeof(MapSegment), true, true, true, true);
        segList.elementHeight = 80;
        DrawSeg();
    }

    private void DrawSeg()
    {
        segList.onAddCallback = (list) =>
        {
            var seg = map.CreatSegment();
            seg.childs = new MapChild[4];
            for (int i = 0; i < seg.childs.Length; i++)
            {
                seg.childs[i] = new MapChild(-1, Vector3.zero, 0);
            }
        };
        segList.drawElementCallback =
            (rect, index, isActive, isFocused) =>
            {
                var r = rect;
                r.y += rect.height * 0.05f;
                r.width = 280;
                r.height = rect.height * 0.3f;
                EditorGUI.IntField(r, "地图号", map.segments[index].mapid);
                r.y += rect.height * 0.35f; r.width = 80;
                EditorGUI.LabelField(r, "空气墙");
                r.x += 80;
                r.width = 200;
                map.segments[index].wall = EditorGUI.Vector2Field(r, string.Empty, map.segments[index].wall);
                if (index == 0)
                {
                    r.y += rect.height * 0.35f; r.width = 80; r.x -= 80;
                    EditorGUI.LabelField(r, "出生点");
                    r.x += 80;
                    r.width = 200;
                    map.segments[index].swpanPos = EditorGUI.Vector3Field(r, string.Empty, map.segments[index].swpanPos);
                }
                var a = map.segments[index].childs;
                DrawSegChild(a,rect);
            };
    }


    private void DrawSegChild(MapChild[] a,Rect rect)
    {
        var r = rect;
        r.y += rect.height * 0.05f;
        var b = r.x += 300;
        for (int i = 0; i < a.Length; i++)
        {
            r.x = b + i * 120;
            r.width = 50;
            r.height = rect.height * 0.3f;

            EditorGUI.LabelField(r, "连接地图");
            r.x += 60;
            r.width = 50;
            a[i].mapid=EditorGUI.IntField(r, a[i].mapid);
            r.x -= 60;
            r.y += rect.height * 0.3f;
            r.width = 120;
            a[i].swpanPos=EditorGUI.Vector3Field(r, string.Empty,a[i].swpanPos);
            r.y -= rect.height * 0.3f;


        }
    }

    private void OnGUI()
    {
        segList.DoLayoutList();
    }
}
