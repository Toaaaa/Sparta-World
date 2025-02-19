using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStonePlayer : MonoBehaviour //2d UI에서의 플레이어 움직임
{
    RectTransform rect;
    float h;
    float lastH;// 벽에 부딫히기전 마지막 입력값.
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
        if (IsOverlappingWithWall() && h == lastH)// 반대 방향의 입력이 아니면 return.
            return;

        lastH = h;
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
        if (IsOverlappingWithWall() && h == lastH)// 반대 방향의 입력이 아니면 return.
            return;

        anim.SetFloat("Horizontal", h);
        anim.SetBool("isMoving", isMoving);

    }

    public void ResetPlayer()// 플레이어 위치 초기화
    {
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, 50);
    }
    bool IsOverlappingWithWall()// 벽과 충돌시 이동 제한.
    {
        Bounds wallL = GetUIBounds(GameManager.Instance.fallingStoneManager.wallL.GetComponent<RectTransform>());
        Bounds wallR = GetUIBounds(GameManager.Instance.fallingStoneManager.wallR.GetComponent<RectTransform>());
        Bounds player = GetUIBounds(rect);
        if (wallL.Intersects(player)||wallR.Intersects(player))
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
