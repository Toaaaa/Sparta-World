using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //플레이어 이동 변수
    float h;
    float v;
    bool isHorizonMove;
    float hori_time;// h인풋 지속시간
    float verti_time;// v인풋 지속시간
    float hori_delta;
    float verti_delta;
    public float speed = 5f;
    public bool isMoving;
    public bool cantAction;

    //플레이어 상호작용
    Vector2 dirVec;// 바라보는 방향
    GameObject scannedObject;// dirVec 방향으로 스캔한 오브젝트

    //플레이어 컴포넌트
    private Rigidbody2D rb;
    private Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMovement();
        InteractScan();
    }

    private void FixedUpdate()
    {
        //플레이어 이동
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rb.velocity = moveVec * speed;
        //레이캐스팅
        //Debug.DrawRay(this.transform.position, dirVec*1.7f, new Color(0, 1, 0));// hit 판정 레이를 보여주는 디버깅용 코드.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dirVec, 0.7f, LayerMask.GetMask("Interactable"));
        scannedObject = hit.collider != null ? hit.collider.gameObject : null;
    }

    void PlayerMovement()// 플레이어의 방향값 설정 (마지막에 입력된 값을 출력 및 한쪽 Direction으로만 힘이 작용하게 제한)
    {
        isMoving = h != 0 || v != 0;// h: -1 왼쪽, +1 오른쪽  v: -1 아래, +1 위
        if (hori_time != 0 && verti_time != 0)//만약 두개가 동시에 눌러지고 있을 경우 마지막에 입력된 값을 출력하기
            CheckTheLastDir();
        else
        {
            h = cantAction ? 0 : Input.GetAxisRaw("Horizontal");
            v = cantAction ? 0 : Input.GetAxisRaw("Vertical");
        }

        bool hDown = cantAction ? false : Input.GetButton("Horizontal");
        bool vDown = cantAction ? false : Input.GetButton("Vertical");
        bool hUp = cantAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = cantAction ? false : Input.GetButtonUp("Vertical");

        hori_delta += Time.deltaTime * (hDown ? 1 : 0);
        verti_delta += Time.deltaTime * (vDown ? 1 : 0);

        if (hUp)
        {
            hori_time = hori_delta = 0;
        }
        if (vUp)
        {
            verti_time = verti_delta = 0;
        }
        if (h == 0 && v == 0)
        {
            verti_delta = hori_delta = 0;
        }

        isHorizonMove = hDown || (hUp || vUp) && h != 0;
        if (hDown && vDown)
            isHorizonMove = hori_time > verti_time;

        // 방향 설정
        if (vDown) dirVec = v == 1 ? Vector3.up : Vector3.down;
        if (hDown) dirVec = h == 1 ? Vector3.right : Vector3.left;

        //움직임 애니메이터        
        anim.SetFloat("Horizontal", h);
        anim.SetFloat("Vertical", v);
        anim.SetBool("isMoving", isMoving);
    }
    void CheckTheLastDir()// 방향 인풋 h,v를 당담하는 입력중 동시에 입력중일떄 마지막으로 입력된 인풋을 우선으로 제공하는 함수.
    {
        bool lastHorizontal = hori_time > verti_time;
        h = lastHorizontal ? (cantAction ? 0 : Input.GetAxisRaw("Horizontal")) : 0;
        v = lastHorizontal ? 0 : (cantAction ? 0 : Input.GetAxisRaw("Vertical"));
    }
    void InteractScan()// 스캔된 오브젝트와 상호작용.
    {
        if(Input.GetButtonDown("Jump") && scannedObject != null&& !cantAction)
        {
            if(scannedObject.tag == "Game1")
            {
                Debug.Log("Game1 시작");
                GameManager.Instance.SetGameName("Game1");
                GameManager.Instance.GameStartUIOn();
                cantAction = true;
            }
            if(scannedObject.tag == "Game2")
            {
                Debug.Log("Game2 시작");
                GameManager.Instance.SetGameName("Game2");
                GameManager.Instance.GameStartUIOn();
                cantAction = true;
            }
            if(scannedObject.tag == "NPC")
            {
                Debug.Log("NPC와 대화");
                scannedObject.GetComponent<NpcTalk>().Talk();
            }
        }
    }
}
