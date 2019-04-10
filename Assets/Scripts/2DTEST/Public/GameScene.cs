using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene
{

    public void LoadScene(string scene)
    {
        GameEvent.App.OnSceneStartJump?.Invoke();
        SceneManager.LoadScene(scene);
        GameEvent.App.OnSceneEndJump?.Invoke(scene);
    }

    public void LoadSceneAsync(string scene)
    {
        GameApp.Instance.StartCoroutine(_loadScene(scene));
    }

    IEnumerator _loadScene(string scene)
    {
        var l = SceneManager.LoadSceneAsync("Loading");
        GameEvent.App.OnSceneStartJump?.Invoke();
        while (!l.isDone)
        {
            yield return null;
        }
        Debugger.Game.Log("Beging Loading  " + scene);
        var load = GameApp.ui.app.Open<SurfaceLoading>();
        while (!load)
        {
            yield return null;
        }
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            load.OnSceneLoading(op.progress);
            yield return null;
        }
        float p = 0;
        while (p < 0.1f)
        {
            p += 0.01f;
            load.OnSceneLoading(op.progress + p);
            yield return null;
        }
        GameApp.ui.app.Close<SurfaceLoading>(true);

        op.allowSceneActivation = true;
        yield return null;                          //跳转场景后发出事件，放在一帧后，防止事件接收不到
        GameEvent.App.OnSceneEndJump?.Invoke(scene);
        GameApp.cameraCt.effect.FadeIn_Black(3);
    }
}
