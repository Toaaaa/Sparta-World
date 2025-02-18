using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour
{
    RectTransform rect;
    [SerializeField] float jumpPower = 110f;
    [SerializeField] float jumpTime = 0.2f;
    [SerializeField] float gravityValue = 350;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (GameManager.Instance.flappyBirdManeger.StartGame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FlyUp();
            }
            GravityDown();
        }
    }
    void FlyUp()
    {
        rect.DOAnchorPosY(rect.anchoredPosition.y + jumpPower, jumpTime)
            .SetEase(Ease.OutQuad);
    }
    void GravityDown()
    {
        rect.anchoredPosition += Vector2.down * gravityValue * Time.deltaTime;
    }
}
