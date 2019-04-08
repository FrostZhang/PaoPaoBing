using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurfaceMap : SurfaceChild {

    public Button close;
    public Button snow;

    private void Start()
    {
        close.onClick.AddListener(() =>
        {
            Close();
        });
        snow.onClick.AddListener(() =>
        {
            Close();
            GameApp.sceneMG.LoadSceneAsync("1");
        });
    }
}
