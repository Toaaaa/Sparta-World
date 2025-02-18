using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenUpDown : MonoBehaviour
{
    public float moveDistance = 2f; // 위아래 이동 거리
    public float moveTime = 1f; // 한 방향으로 이동하는 시간

    private void Start()
    {
        StartVerticalMovement();
    }

    private void StartVerticalMovement()
    {
        transform.DOMoveY(transform.position.y + moveDistance, moveTime)
            .SetEase(Ease.InOutSine) // 부드럽게 이동
            .SetLoops(-1, LoopType.Yoyo); // 무한 반복 (위-아래-위-아래)
    }
}
