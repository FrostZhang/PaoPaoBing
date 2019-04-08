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
        GameEvent.App.OnSceneEndJump?.Invoke();
    }

    public void LoadSceneAsync(string scene)
    {
        GameEvent.App.OnSceneStartJump?.Invoke();
        GameApp.Instance.StartCoroutine(_loadScene(scene));
    }

    IEnumerator _loadScene(string scene)
    {
        var l = SceneManager.LoadSceneAsync("Loading");
        while (!l.isDone)
        {
            yield return null;
        }
        Debugger.Game.Log("Beging Loading  " + scene);
        var load= GameApp.ui.app.Open<SurfaceLoading>();
        while (!load)
        {
            yield return null;
        }
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            load.OnSceneLoading(op.progress);
            //GameEvent.App.OnSceneLoading?.Invoke(op.progress);
            yield return null;
        }
        float p = 0;
        while (p < 0.1f)
        {
            p += 0.01f;
            load.OnSceneLoading(op.progress + p);
            //GameEvent.App.OnSceneLoading?.Invoke(op.progress + p);
            yield return null;
        }
        GameApp.ui.app.Close<SurfaceLoading>(true);
        op.allowSceneActivation = true;
        GameEvent.App.OnSceneEndJump?.Invoke();
    }
}
