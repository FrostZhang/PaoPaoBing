using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurfaceBag : SurfaceChild
{
    public Button close;

    public Transform bagPa;
    private Transform prefab;

    public List<Transform> items;
    protected override void Awake()
    {
        base.Awake();
        prefab = bagPa.GetChild(1);
        prefab.gameObject.SetActive(false);
        items = new List<Transform>();
    }

    void Start()
    {
        close.onClick.AddListener(() =>
        {
            go.SetActive(false);
        });
    }

    void LoadBagAll()
    {
        var a = GameApp.playerData.bagitems;
        for (int i = 0; i < a.Count; i++)
        {
            var t = Instantiate(prefab, bagPa);
            t.name = i.ToString();
            t.gameObject.SetActive(true);
            items.Add(t);
            var tg = t.GetComponent<Toggle>();
            tg.onValueChanged.AddListener((_) =>
            {
                if (_)
                {

                }
            });
        }
    }

    void AdjustLengh(int number)
    {

    }

    private void ShowDetail(int i)
    {

    }

}
