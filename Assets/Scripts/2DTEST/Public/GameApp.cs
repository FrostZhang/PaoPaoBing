using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : Singleton<GameApp>
{
    //controller;

    public static GameObjectPool pool;
    public static CamaraFollow cameraCt;
    public static GameTimer gameTimer;
    public static GameEvent gameEvent;

    public GameMap mapController;
    public GameObject[] prefab;
    public Canvas canvas;
    public Canvas hub;
    public Player2D player2D;

    void Awake()
    {
        pool = new GameObjectPool(prefab);
        cameraCt = FindObjectOfType<CamaraFollow>();
        gameTimer = new GameTimer();
        player2D = FindObjectOfType<Player2D>();
        mapController = FindObjectOfType<GameMap>();
        DontDestroyOnLoad(this);
        Application.lowMemory += Application_lowMemory;
        Application.onBeforeRender += Application_onBeforeRender;
#if UNITY_2018_1
        Application.wantsToQuit += Application_wantsToQuit;
        Application.quitting += Application_quitting;
#endif
        Application.runInBackground = true;
        Application.targetFrameRate = 30;

        Debugger.App.Log("GameApp 初始化完成");
        //Debugger.Game.Log("GameApp 初始化完成");
        //Debugger.Network.Log("GameApp 初始化完成");
        //Debugger.Resource.Log("GameApp 初始化完成");
        //Debugger.UI.Log("GameApp 初始化完成");
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

    float t = 3;

    void Update()
    {
        gameTimer.Update();
    }
}