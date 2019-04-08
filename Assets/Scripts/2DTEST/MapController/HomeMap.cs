using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeMap : MonoBehaviour
{
    public SuperTextMesh wacth;
    public PointerHandel mission;
    public PointerHandel map;

    // Use this for initialization
    void Start()
    {
        var a = Resources.Load<Player2D>("Model/Player");
        if (a)
        {
            var _ = Instantiate(a);
            GameApp.cameraCt.target = _.tr;
        }
        GameApp.cameraCt.LimitCaViewH(-4.99f, 27.12f);

        mission.action = () =>
        {
            OnClickMission();
        };
        map.action = () =>
        {
            GameApp.ui.game.Open<SurfaceMap>();
        };

        LondBaseUI();
    }

    private void LondBaseUI()
    {
        GameApp.ui.game.Open<SurfaceHead>();
        GameApp.ui.game.Open<SurfaceMenu>();
        SuperTextMesh.RebuildAll();
        HPTextController hc;
        if (GameApp.ui.hub.TryGet(out hc))
        {
            hc.Clear();
        }
        else
        {
            GameApp.ui.hub.Open<HPTextController>();
        }
    }

    public void OnClickMission()
    {
        GameApp.ui.game.Open<MissionUI>();
    }

    void Update()
    {
        UpWacth();
    }

    private void UpWacth()
    {
        wacth.text = DateTime.Now.ToShortTimeString();
    }
}
