using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace Talk
{
    public class SchoolZone : MonoBehaviour
    {
        public Image sr544;
        public Image sr584;
        public Image dajie;
        public Image im21;

        SurfaceTalk SurfaceTalk;

        int skip = 2;
        int ok = 4;
        int refuse = 8;


        void Start()
        {
            SurfaceTalk = GameApp.ui.app.Open<SurfaceTalk>();
            ReadPlot();
            GameEvent.App.OnShowPlot += OnShowPlot;
            GameEvent.App.OnPlotEnd += OnPlotEnd;
        }

        private void OnDestroy()
        {
            GameEvent.App.OnShowPlot -= OnShowPlot;
            GameEvent.App.OnPlotEnd -= OnPlotEnd;
        }

        private void OnPlotEnd()
        {
            GameApp.ui.app.Close<SurfaceTalk>();
            GameApp.sceneMG.LoadSceneAsync("GCity");
        }

        private void ReadPlot()
        {
            SurfaceTalk.SetPlots(GameApp.playerData.PlotDatas1, 0);
        }

        private void OnShowPlot(int plotID)
        {
            switch (plotID)
            {
                case 1:
                    sr544.DOFade(1, 2f);
                    break;
                case 4:
                    sr544.gameObject.SetActive(false);
                    sr584.DOFade(1, 2f);
                    break;
                case 9:
                    sr584.gameObject.SetActive(false);
                    break;
                case 10:
                    dajie.DOFade(1, 2f).OnComplete(() =>
                    {
                        im21.gameObject.SetActive(false);
                    });
                    break;
                default:
                    break;
            }
        }
    }
}
