using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurfaceTalk : SurfaceChild
{
    public SuperTextMesh chat;
    public Button skip;
    public Button ok;
    public Button refuse;

    List<PlotData> plotDatas;
    private GameObject skipG;
    private GameObject okG;
    private GameObject refuseG;

    protected override void Awake()
    {
        base.Awake();
        skipG = skip.gameObject;
        okG = ok.gameObject;
        refuseG = refuse.gameObject;
    }

    public void SetPlots(List<PlotData> datas,int startplotID)
    {
        plotDatas = datas;
        Show(startplotID);
    }

    public void SetPlotAt(int plotID)
    {
        Show(plotID);
    }

    private void Show(int plotID)
    {
        Reset();
        var plo = plotDatas.Find((_) => _.plotID == plotID);
        if (plo == null)
        {
            Debugger.Game.LogError("剧情未找到 ID " + plotID);
            return;
        }
        chat.text = plo.chatstr;
        chat.onCompleteEvent.AddListener(() =>
        {
            Debugger.Game.Log("播放完毕");
            int i = plo.btns;
            if ((i & 2) == 2)
            {
                skipG.SetActive(true);
                if (plo.okOrSkip == 0)
                {
                    skip.onClick.AddListener(() => { GameEvent.App.OnPlotEnd?.Invoke();Close(); });
                }
                else
                    skip.onClick.AddListener(() => ShowNext(plo.okOrSkip));
            }
            if ((i & 4) == 4)
            {
                okG.SetActive(true);
                if (plo.okOrSkip == 0)
                {
                    ok.onClick.AddListener(() => { GameEvent.App.OnPlotEnd?.Invoke(); Close(); });
                }
                else
                    ok.onClick.AddListener(() => ShowNext(plo.okOrSkip));
            }
            if ((i & 8) == 8)
            {
                refuseG.SetActive(true);

                if (plo.refuse == 0)
                {
                    refuse.onClick.AddListener(() => { GameEvent.App.OnPlotEnd?.Invoke(); Close(); });
                }
                else
                    refuse.onClick.AddListener(() => ShowNext(plo.refuse));
            }
        });
    }

    private void ShowNext(int plotID)
    {
        Show(plotID);
        GameEvent.App.OnShowPlot?.Invoke(plotID);
    }

    public void Reset()
    {
        skip.onClick.RemoveAllListeners();
        ok.onClick.RemoveAllListeners();
        refuse.onClick.RemoveAllListeners();
        chat.onCompleteEvent.RemoveAllListeners();
        chat.text = string.Empty;
        skipG.SetActive(false);
        okG.SetActive(false);
        refuseG.SetActive(false);
    }

}
