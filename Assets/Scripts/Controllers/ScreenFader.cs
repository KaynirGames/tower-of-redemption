using System.Collections;
using UnityEngine;

public static class ScreenFader
{
    public static IEnumerator FadeIn(CanvasGroup canvasGroup, bool unscaled = false)
    {
        canvasGroup.alpha = 0;

        float delta = unscaled
            ? Time.unscaledDeltaTime
            : Time.deltaTime;

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += delta;
            yield return null;
        }
    }

    public static IEnumerator FadeOut(CanvasGroup canvasGroup, bool unscaled = false)
    {
        canvasGroup.alpha = 1;

        float delta = unscaled
            ? Time.unscaledDeltaTime
            : Time.deltaTime;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= delta;
            yield return null;
        }
    }
}
