using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : Singleton<GameApp>
{
    //controller;

    public static GameObjectPool pool;
    public static CamaraFollow cameraCt;
    public static GameTimer gameTimer;

    public GameMap mapController;

    public GameObject[] prefab;
    public Canvas canvas;
    public Canvas hub;
    public Player2D player2D;

    void Awake ()
    {
        pool = new GameObjectPool(prefab);
        cameraCt = FindObjectOfType<CamaraFollow>();
        gameTimer = new GameTimer();
        player2D = FindObjectOfType<Player2D>();
        mapController = FindObjectOfType<GameMap>();
        DontDestroyOnLoad(this);
        Application.lowMemory += Application_lowMemory;
        Application.onBeforeRender += Application_onBeforeRender;
        Application.wantsToQuit += Application_wantsToQuit;
        Application.quitting += Application_quitting;
        Application.runInBackground = true;
        Application.targetFrameRate = 30;

        Debugger.App.Log(123213);
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
        
    }

    float t = 3;

	void Update () {
        gameTimer.Update();
    }
}