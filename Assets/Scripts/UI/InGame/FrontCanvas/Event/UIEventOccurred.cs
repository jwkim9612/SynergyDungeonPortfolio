using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIEventOccurred : MonoBehaviour
{
    [SerializeField] private Text eventOccurredText = null;
    [SerializeField] private float fadeTime = 0.0f;
    [SerializeField] private float stopTime = 0.0f;
    [SerializeField] private int numOfFlash = 0;

    /// <summary>
    /// 애니메이션 시작
    /// </summary>
    /// <returns>애니메이션 시간</returns>
    public float PlayAnimation()
    {
        MakeTextTransparent();
        OnShow();
        StartCoroutine(Co_PlayAnimation());

        return (fadeTime * numOfFlash) + (stopTime * numOfFlash);
    }

    public IEnumerator Co_PlayAnimation()
    {
        float time = 0.0f;

        for (int i = 0; i < numOfFlash; i++)
        {
            float halfFadeTime = fadeTime / 2;

            while (time < halfFadeTime)
            {
                eventOccurredText.color = new Color(0, 0, 0, 0.0f + time / halfFadeTime);
                time += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(stopTime);
            time = 0.0f;

            while (time < halfFadeTime)
            {
                eventOccurredText.color = new Color(0, 0, 0, 1.0f - time / halfFadeTime);
                time += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            time = 0.0f;
        }

        OnHide();
        yield break;
    }

    private void MakeTextTransparent()
    {
        eventOccurredText.color = new Color(1, 1, 1, 0.0f);
    }

    private void OnShow()
    {
        this.gameObject.SetActive(true);
    }

    private void OnHide()
    {
        this.gameObject.SetActive(false);
    }
}
