using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Utility : Editor
{

    [MenuItem(@"Selction/去掉anim  meshclider")]
    public static void GetTransforms()
    {
        Transform[] transforms = Selection.transforms;
        digui(transforms[0]);
    }

    private static void digui(Transform tr)
    {
        for (int i = 0; i < tr.childCount; i++)
        {
            MeshCollider mc = tr.GetChild(i).GetComponent<MeshCollider>();
            if (mc != null)
            {
                DestroyImmediate(mc);
            }
            Animation at = tr.GetChild(i).GetComponent<Animation>();
            if (at != null && at.GetClipCount() == 0)
            {
                DestroyImmediate(at);
            }
            digui(tr.GetChild(i));
        }
    }

}
