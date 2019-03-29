using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent
{
    public class PlayerData
    {
        public static Action<int,int> OnChange_HP;
        public static Action<int,int> OnChange_MP;
        public static Action<int> OnChange_Level;
        public static Action<int> OnChange_Att;
    }



}
