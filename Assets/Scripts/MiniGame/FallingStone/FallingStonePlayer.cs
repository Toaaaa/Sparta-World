using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStonePlayer : MonoBehaviour //2d UI에서의 플레이어 움직임
{
    RectTransform rect;
    float h;
    float hori_delta;
    public float speed = 430;
    public bool isMoving;// false일때는 정면을 바라보는 상태.
    public bool cantAction;// 게임 오버 또는 대기시 움직임 제한.

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

    }
    private void FixedUpdate()
    {
        Vector2 moveVec = new Vector2(h, 0);
        rect.anchoredPosition += moveVec * speed * Time.deltaTime;
    }
    void PlayerMovement()
    {
        isMoving = h != 0;
        h = cantAction ? 0 : Input.GetAxisRaw("Horizontal");
        bool hDown = cantAction ? false : Input.GetButton("Horizontal");
        hori_delta += Time.deltaTime * (hDown ? 1 : 0);

        if (h == 0)
            hori_delta = 0;

        // 애니메이터 설정 (수직 입력 제거)
        anim.SetFloat("Horizontal", h);
        anim.SetBool("isMoving", isMoving);

    }

    public void ResetPlayer()// 플레이어 위치 초기화
    {

    }
}
