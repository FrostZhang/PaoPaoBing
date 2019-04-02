using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour {

    public Button close;
    public Transform pa;
    private Transform prefab;

    bool isStart;
	void Start ()
    {
        if (isStart)
        {
            return;
        }
        close.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        prefab = pa.GetChild(0);
        prefab.gameObject.SetActive(false);
        isStart = true;
    }

    private void OnEnable()
    {
        if (!isStart)
        {
            Start();
        }
    }

    private void OnDestroy()
    {
        close.onClick.RemoveAllListeners();
    }
}
