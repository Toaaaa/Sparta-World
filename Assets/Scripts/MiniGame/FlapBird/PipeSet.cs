using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSet : MonoBehaviour
{
    RectTransform rectTransform;
    Vector2 originalRect;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalRect = rectTransform.anchoredPosition;
    }

    public void SetPipe()
    {
        //rectTransform의 y값 랜덤
        float y = Random.Range(-250f, 250f);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);       
    }
    public void StartMoving()
    {
        DOTween.To(() => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x, new Vector2(-1700, rectTransform.anchoredPosition.y), 5f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                rectTransform.anchoredPosition = originalRect;
                this.gameObject.SetActive(false);
            });
    }
    public void StopMoving()
    {
        DOTween.Pause(rectTransform);
    }
}
