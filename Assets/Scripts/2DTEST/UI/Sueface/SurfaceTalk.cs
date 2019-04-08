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

    public void SetPlot(List<PlotData> datas)
    {
        plotDatas = datas;
        Show(datas[0].plotID);
    }

    private void Show(int plotID)
    {
        Reset();
        var plo = plotDatas.Find((_) => _.plotID == plotID);
        if (plo==null)
        {
            Debugger.Game.LogError("剧情未找到 ID " + plotID);
            return;
        }
        chat.text = plo.chatstr;
        chat.onCompleteEvent.AddListener(() =>
        {
            if (plo.okOrSkip == 0)
            {
                GameEvent.App.OnPlotEnd?.Invoke();
                //退出
            }
            else
                skip.onClick.AddListener(() => ShowNext(plo.okOrSkip));
            if (plo.refuse == 0)
            {
                GameEvent.App.OnPlotEnd?.Invoke();
                //退出
            }
            else
                refuse.onClick.AddListener(() => ShowNext(plo.refuse));

            int i = plo.btns;
            if ((i & 2) == 2)
            {
                skipG.SetActive(true);
            }
            if ((i & 4) == 4)
            {
                okG.SetActive(true);
            }
            if ((i & 8) == 8)
            {
                refuseG.SetActive(true);
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
        chat.text = string.Empty;
        skip.onClick.RemoveAllListeners();
        ok.onClick.RemoveAllListeners();
        refuse.onClick.RemoveAllListeners();
        skipG.SetActive(false);
        okG.SetActive(false);
        refuseG.SetActive(false);
    }

}
