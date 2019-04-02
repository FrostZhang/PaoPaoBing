using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaEffect : MonoBehaviour
{
    public Material ma;

    private Material _ma;
    static string Brightness = "_Brightness";

    private void Start()
    {
        _ma = new Material(ma);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _ma);
    }

    public void FadeOut_Black(float t = 2f)
    {
        StartCoroutine(_FadeOut_Black(t));
    }

    public void FadeIn_Black(float t = 2f)
    {
        StartCoroutine(_FadeIn_Black(t));
    }

    IEnumerator _FadeOut_Black(float t)
    {
        while (_ma.GetFloat(Brightness) > 0.05f)
        {
            _ma.SetFloat(Brightness, _ma.GetFloat(Brightness) - 0.02f);
            yield return null;
        }
        _ma.SetFloat(Brightness, 0);
        GameEvent.App.OnCaFadeOut?.Invoke();
    }

    IEnumerator _FadeIn_Black(float t)
    {
        while (_ma.GetFloat(Brightness) < 0.95f)
        {
            _ma.SetFloat(Brightness, _ma.GetFloat(Brightness) + 0.02f);
            yield return null;
        }
        _ma.SetFloat(Brightness, 1);
        GameEvent.App.OnCaFadeIn?.Invoke();
    }
}
