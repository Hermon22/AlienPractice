using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowsController : MonoBehaviour
{
    [Header("Main Window Object")]
    [SerializeField] private GameObject window;
    
    [Header("Window Elements")]
    [Space(10)]
    [SerializeField] private Image[] windowImages;


    [Header("Window Animations")]
    [Space(10)]
    [SerializeField] private AnimationCurve timeCurve;
    [SerializeField] private float timeToFade = 0.5f;
    
    [ContextMenu("OPE")]
    public void OpenWindow()
    {
        ExpandElements();
    }
    [ContextMenu("clo")]
    public void CloseWindow()
    {
        FadeOut();
    }

    private void ExpandElements()
    {
        window.SetActive(true);
        StartCoroutine(ScaleWindow(true));
    }
    private void ShrinkElements()
    {
        StartCoroutine(ScaleWindow(false));
    }
    
    private IEnumerator ScaleWindow(bool openWin)
    {
        float t = 0;
        do
        {
            t += Time.deltaTime / timeToFade;
            window.transform.localScale = openWin ? Vector3.Lerp(Vector3.zero, Vector3.one, timeCurve.Evaluate(t)) : Vector3.Lerp(Vector3.one, Vector3.zero, timeCurve.Evaluate(t));
            yield return null;
        } while (t < 1f);
        if (openWin)
            FadeIn();
        else
            window.SetActive(false);
    }
    
    private void FadeIn()
    {
        if (windowImages.Length <= 0) return;
        foreach (var winImage in windowImages)
        {
            StartCoroutine(Fade(true,winImage));
        }
    }
    
    private void FadeOut()
    {
        if (windowImages.Length <= 0) return;
        foreach (var winImage in windowImages)
        {
            StartCoroutine(Fade(false,winImage));
        }
    }

    private IEnumerator Fade(bool fadeIn, Image winImage)
    {
        float t = 0;
        do
        {
            t += Time.deltaTime / timeToFade;
            if (fadeIn)
            {
                var tmpCol = winImage.color;
                tmpCol.a = Mathf.Lerp(0, 1, timeCurve.Evaluate(t));
                winImage.color = tmpCol;
            }
            else
            {
                var tmpCol = winImage.color;
                tmpCol.a = Mathf.Lerp(1, 0, timeCurve.Evaluate(t));
                winImage.color = tmpCol;
            }
            yield return null;
        } while (t < 1f);
        if(!fadeIn)
            ShrinkElements();
    }
}
