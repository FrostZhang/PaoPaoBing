using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSwpan
{
    public GameObject[] prefabs;

    public void Swpan(int id)
    {
        Resources.Load<RuntimeAnimatorController>(string.Format("Animator/1 ({0})",id));
    }
}
