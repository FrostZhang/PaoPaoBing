using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace Talk
{
    public class Talk2 : MonoBehaviour
    {
        public Image im601;
        public Image im602;
        public Image bg31;
        public Image bg32;
        public Image bg34;
        public Image im102;
        public Image im103;

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
            GameApp.sceneMG.LoadSceneAsync("GCity");
            GameEvent.App.OnSceneEndJump += OnSceneEndJump;
        }

        private void OnSceneEndJump(string sc)
        {
            if (sc== "GCity")
            {
                GameApp.ui.app.Open<SurfaceTalk>().SetPlots(GameApp.playerData.PlotDatas1, 50);
            }
            GameEvent.App.OnSceneEndJump -= OnSceneEndJump;
        }

        private void ReadPlot()
        {
            SurfaceTalk.SetPlots(GameApp.playerData.PlotDatas1, 21);//21
        }

        private void OnShowPlot(int plotID)
        {
            switch (plotID)
            {
                case 25:
                    im602.DOFade(1, 2f).OnComplete(() =>
                    {
                        im601.gameObject.SetActive(false);
                    });
                    break;
                case 26:
                    im602.DOFade(0, 1).OnComplete(() => { im602.gameObject.SetActive(false); });
                    bg32.DOFade(1, 2f).OnComplete(() =>
                    {
                        bg31.gameObject.SetActive(false);
                    });
                    break;
                case 29:
                    im102.DOFade(1, 2);
                    break;
                case 33:
                    im102.gameObject.SetActive(false);
                    GameApp.cameraCt.effect.FadeIn_Black();
                    bg34.DOFade(1, 2f).OnComplete(() =>
                    {
                        bg32.gameObject.SetActive(false);
                        im103.DOFade(1, 2);
                    });
                    break;
                default:
                    break;
            }
        }
    }
}
