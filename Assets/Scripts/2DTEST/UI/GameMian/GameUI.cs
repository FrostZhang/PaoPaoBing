using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI系统由三个canvas组成
//hub 用于显示血条，名字
//game 用于显示界面交互（比如：背包）
//App  用于系统通知（比如：弹出窗）

public class GameUI : MonoBehaviour
{
    public SurfaceContainer hub;
    public SurfaceContainer game;
    public SurfaceContainer app;

    private void Awake()
    {
        GameEvent.App.OnSceneStartJump += OnSceneSJump;
        GameEvent.App.OnSceneEndJump += OnSceneEJump;
    }

    private void OnDestroy()
    {
        GameEvent.App.OnSceneStartJump -= OnSceneSJump;
        GameEvent.App.OnSceneEndJump -= OnSceneEJump;
    }

    private void OnSceneEJump(string sc)
    {
        game.OpenAll();
        //解决跳转场景带来的supermesh不显示bug
        GameApp.gameTimer.Delay(() => { SuperTextMesh.RebuildAll(); }, 0.1f, true);
    }

    private void OnSceneSJump()
    {
        app.CloseAll(true);
        game.CloseAll(true);
        hub.CloseAll(false);
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }

}
