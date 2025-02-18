using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreCheck : MonoBehaviour
{
    RectTransform rect;
    bool scoreCheck = false;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (IsOverlappingWIthFlyer())
        {
            scoreCheck = true;
        }
        else if (scoreCheck)
        {
            GameManager.Instance.flappyBirdManeger.Score++;
            scoreCheck = false;
        }
    }
    bool IsOverlappingWIthFlyer()
    {
        Bounds bound = GetUIBounds(rect);
        Bounds flyer = GetUIBounds(GameManager.Instance.flappyBirdManeger.flyer.GetComponent<RectTransform>());

        if (bound.Intersects(flyer))
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
