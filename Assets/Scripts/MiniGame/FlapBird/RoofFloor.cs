using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofFloor : MonoBehaviour
{
    public RectTransform roof;
    public RectTransform floor;

    private void Update()
    {
        if(IsOverlappingWithRoofFloor())
        {
            GameManager.Instance.flappyBirdManeger.StopGame();
        }
    }

    bool IsOverlappingWithRoofFloor()
    {
        Bounds roofBound = GetUIBounds(roof);
        Bounds floorBound = GetUIBounds(floor);

        Bounds flyer = GetUIBounds(GameManager.Instance.flappyBirdManeger.flyer.GetComponent<RectTransform>());
        if (roofBound.Intersects(flyer) || floorBound.Intersects(flyer)) // 충돌 여부 확인
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
