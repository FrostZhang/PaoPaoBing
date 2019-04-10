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
        public static Action<int> OnChange_Exp;
        public static Action<int> OnChange_Att;
        public static Action<int> OnChange_Coin;
        public static Action<BagItem> OnAdd_BagItem;
        public static Action<BagItem> OnDelete_BagItem;
    }

    public class App
    {
        public static Action OnCaFadeOut;
        public static Action OnCaFadeIn;
        public static Action<float> OnSceneLoading;
        public static Action OnSceneStartJump;
        public static Action<string> OnSceneEndJump;

        public static Action<int> OnShowPlot;
        public static Action OnPlotEnd;
    }

    public class HPText
    {
        public static Func<Transform, string, HpUI> InstansHpUI;
        public static Action<Vector3, string> ShowDYUI;
        public static Action<HpUI> RecycleUI;

    }
}
