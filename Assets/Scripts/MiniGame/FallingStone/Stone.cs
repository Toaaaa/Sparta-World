using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    RectTransform rect;
    public float moveTime = 5f;

    Vector2 originalRect;
    Tween thisTween;

    private void OnEnable()
    {
        if(rect != null)
            rect = GetComponent<RectTransform>();
        ResizeStone();
    }
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        originalRect = rect.anchoredPosition;
    }
    public void StartMoving()
    {
        Debug.Log("StartMoving");
        thisTween = DOTween.To(() => rect.anchoredPosition, y =>
        {
            rect.anchoredPosition = y;
            if (IsOverlappingWithPlayer())// 파이프와 충돌시 게임 종료.
                GameManager.Instance.fallingStoneManager.StopGame();
        },
            new Vector2(rect.anchoredPosition.x, -1000), moveTime)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                rect.anchoredPosition = originalRect;
                this.gameObject.SetActive(false);
            });
    }
    bool IsOverlappingWithPlayer()
    {
        Bounds player = GetUIBounds(GameManager.Instance.fallingStoneManager.player.GetComponent<RectTransform>());
        Bounds stone = GetUIBounds(rect);
        if (player.Intersects(stone)) // 충돌 여부 확인
        {
            return true;
        }
        return false;
    }
    void ResizeStone()
    {
        float timeScale = 1+ GameManager.Instance.fallingStoneManager.Score * 0.02f;
        float randomScale = Random.Range(0.8f, 1.4f);
        rect.localScale = new Vector3(randomScale*timeScale, randomScale*timeScale, 1f);
    }
    public void StopMoving()
    {
        if (thisTween != null)
            thisTween.Pause();
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
