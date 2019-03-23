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
    }
    float t = 3;

	void Update () {
        gameTimer.Update();
    }
}