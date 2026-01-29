using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private Coroutine currentFade;

    private void Awake()
    {
        // Ensure we start visible (black)
        SetAlpha(1f);
    }

    public void FadeOut()
    {
        StartFade(1f);
    }

    public void FadeIn()
    {
        StartFade(0f);
    }

    public IEnumerator FadeOutCoroutine()
    {
        yield return StartFadeRoutine(1f);
    }

    public IEnumerator FadeInCoroutine()
    {
        yield return StartFadeRoutine(0f);
    }

    private void StartFade(float targetAlpha)
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(StartFadeRoutine(targetAlpha));
    }

    private IEnumerator StartFadeRoutine(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(targetAlpha);
    }

    private void SetAlpha(float alpha)
    {
        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }
}
