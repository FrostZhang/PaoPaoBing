using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SchoolZone : MonoBehaviour
{
    public SpriteRenderer sr544;

    SurfaceTalk SurfaceTalk;

    int skip = 2;
    int ok = 4;
    int refuse = 8;


    void Start()
    {
        if (!GameApp.ui.game.Contains<SurfaceTalk>())
        {
            SurfaceTalk = GameApp.ui.game.Open<SurfaceTalk>();
        }
        else
        {
            GameApp.ui.game.TryGet(out SurfaceTalk);
        }
        ReadPlot();
        GameEvent.App.OnShowPlot += OnShowPlot;
    }

    private void ReadPlot()
    {
        var a1 = new PlotData();
        a1.plotID = 0;
        a1.chatstr = "又是美好的一天呀！";
        a1.btns = skip;
        a1.okOrSkip = 1;

        var a2 = new PlotData();
        a2.plotID = 1;
        a2.chatstr = "咦，小Y好像有心事";
        a2.btns = skip;
        a2.okOrSkip = 2;

        List<PlotData> plotDatas = new List<PlotData>();

        plotDatas.Add(a1);
        plotDatas.Add(a2);

        SurfaceTalk.SetPlot(plotDatas);
    }

    private void OnShowPlot(int plotID)
    {
        switch (plotID)
        {
            case 1:
                sr544.DOFade(1, 2f);
                break;
            case 2:
                sr544.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
