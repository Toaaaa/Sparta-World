using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSet : MonoBehaviour
{
    RectTransform rectTransform;// 파이프 세트의 RectTransform
    public RectTransform pipe1;// 상단 파이프의 RectTransform
    public RectTransform pipe2;// 하단 파이프의 RectTransform

    Vector2 originalRect;
    Tween thisTween;


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
        thisTween = DOTween.To(() => rectTransform.anchoredPosition, x => 
        {
            rectTransform.anchoredPosition = x;
            if(IsOverlappingWithPipes())// 파이프와 충돌시 게임 종료.
                GameManager.Instance.flappyBirdManeger.StopGame();
        },
            new Vector2(-1700, rectTransform.anchoredPosition.y), 5f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                rectTransform.anchoredPosition = originalRect;
                this.gameObject.SetActive(false);
            });
    }
    public void StopMoving()
    {
        if(thisTween != null)
            thisTween.Pause();
    }

    bool IsOverlappingWithPipes()
    {
        Bounds Bound1 = GetUIBounds(pipe1);
        Bounds Bound2 = GetUIBounds(pipe2);

        Bounds flyer = GetUIBounds(GameManager.Instance.flappyBirdManeger.flyer.GetComponent<RectTransform>());
        if (Bound1.Intersects(flyer) || Bound2.Intersects(flyer)) // 충돌 여부 확인
        {
            return true;
        }
        return false;
    }
    Bounds GetUIBounds(RectTransform rect)
    {
        Vector3[] corners = new Vector3[4];
        rect.GetWorldCorners(corners);

        Vector3 center = (corners[0] + corners[2]) / 2f;
        Vector3 size = new Vector3(
            Mathf.Abs(corners[2].x - corners[0].x),
            Mathf.Abs(corners[2].y - corners[0].y),
            1f
        );

        return new Bounds(center, size);
    }
}
