using UnityEngine;
using System.Collections;

public abstract class PanelBase : MonoBehaviour
{
    private Coroutine fadeCoroutine;
    public float baseFadeDuration = 1.0f;

    public virtual void RefreshPanel() { }

    public virtual void ShowPanel(bool show)
    {
        if (!show && !gameObject.activeSelf)
            return;

        if (show && !gameObject.activeSelf)
            gameObject.SetActive(true);

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }

        fadeCoroutine = StartCoroutine(FadeCanvasGroup(show, baseFadeDuration));
    }

    private IEnumerator FadeCanvasGroup(bool show, float duration)
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        if (cg == null)
        {
            Debug.LogWarning($"Missing CanvasGroup on {gameObject.name}");
            yield break;
        }

        float start = cg.alpha;
        float end = show ? 1 : 0;
        float time = 0;

        if (show)
        {
            gameObject.SetActive(true);
            RefreshPanel();
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

        while (time < duration)
        {
            time += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, time / duration);
            yield return null;
        }

        cg.alpha = end;

        if (!show)
        {
            cg.interactable = false;
            cg.blocksRaycasts = false;
            gameObject.SetActive(false);
        }

        fadeCoroutine = null;
    }
}
