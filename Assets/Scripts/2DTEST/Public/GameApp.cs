using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using FSM;
using SysFlow;

public class GameApp : Singleton<GameApp>
{
    //controller;
    public static GameObjectPool pool;  //对象池
    public static GameTimer gameTimer;  //计时器
    public static GameUI ui;            //UI
    public static CamaraFollow cameraCt;//摄像机
    public static FSMController sysFlow;//系统流程
    public static GameScene sceneMG;    //场景管理
    public static CustomData playerData;//玩家资料

    //test
    public GameMap mapController;
    public GameObject[] prefab;

    void Awake()
    {
        pool = new GameObjectPool(prefab);
        cameraCt = GetComponentInChildren<CamaraFollow>();
        gameTimer = new GameTimer();
        ui = GetComponentInChildren<GameUI>();
        sceneMG = new GameScene();
        playerData = new CustomData();

        //开始游戏进程
        var fsm = new FSMApp(transform);
        sysFlow = fsm.Sysflow;

        mapController = FindObjectOfType<GameMap>();
        DontDestroyOnLoad(this);

        RegisterApplicationEvent();
    }

    private void RegisterApplicationEvent()
    {
        Application.lowMemory += Application_lowMemory;
        Application.onBeforeRender += Application_onBeforeRender;
#if UNITY_2018_1
        Application.wantsToQuit += Application_wantsToQuit;
        Application.quitting += Application_quitting;
#endif
        Application.runInBackground = true;
        Application.targetFrameRate = 30;
    }

    private string GetSys()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("GameApp 初始化完成\n");
        sb.Append("GamePool " + pool.ToString() + "\n");
        sb.Append("camera   " + cameraCt.ToString() + "\n");
        sb.Append("GameTimer    " + gameTimer.ToString() + "\n");
        sb.Append("GameUI   " + ui.ToString() + "\n");
        return sb.ToString();
    }

    private void Application_quitting()
    {

    }

    private bool Application_wantsToQuit()
    {
        return true;
    }

    private void Application_onBeforeRender()
    {

    }

    private void Application_lowMemory()
    {
        Debugger.App.LogWarning("GameApp 系统运行内存过低!");
    }

    void Update()
    {
        gameTimer.Update();
    }
}