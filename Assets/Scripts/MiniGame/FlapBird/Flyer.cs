using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour
{
    [SerializeField]RectTransform rect;
    [SerializeField] float jumpPower = 110f;
    [SerializeField] float jumpTime = 0.2f;
    [SerializeField] float gravityValue = 350;
    AudioSource jumpSound;

    private void Awake()
    {
        jumpSound = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (GameManager.Instance.flappyBirdManeger.StartGame)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                FlyUp();
                jumpSound.Play();
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

    public void ResetFlyer()
    {
        rect.anchoredPosition = new Vector2(-430, 0);
    }
}
