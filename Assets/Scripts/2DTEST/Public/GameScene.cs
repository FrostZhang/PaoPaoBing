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
        SceneManager.LoadScene("Loading");
        GameApp.Instance.StartCoroutine(_loadScene(scene));
        GameEvent.App.OnSceneEndJump?.Invoke();
    }

    IEnumerator _loadScene(string scene)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            GameEvent.App.OnSceneLoading?.Invoke(op.progress);
            yield return null;
        }
        float p = 0;
        while (p < 0.1f)
        {
            p += 0.01f;
            GameEvent.App.OnSceneLoading?.Invoke(op.progress + p);
            yield return null;
        }
        yield return new WaitForSeconds(5);
        op.allowSceneActivation = true;
    }
}
