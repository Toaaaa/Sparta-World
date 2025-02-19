using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIMotion : MonoBehaviour
{
    //UI가 열리고 닫힐때의 효과를 보여줄 때 공통으로 사용할 스크립트.

    RectTransform panel;
    public float openDuration;
    public float closeDuration = 0.5f; // 닫힐 때 애니메이션 시간

    public List<Button> buttons;

    private Vector2 originalSize;

    private void Awake()
    {
        panel = GetComponent<RectTransform>();
        originalSize = panel.sizeDelta;  // 원래 사이즈 저장
        panel.sizeDelta = new Vector2(originalSize.x, 0); // 처음에는 가로 막대 형태로
    }
    private void OnEnable()
    {
        OpenPanel();
        StartCoroutine(SetButtons());// 버튼 이미지 활성화.
    }
    public void OpenPanel()
    {
        panel.DOSizeDelta(originalSize, openDuration).SetEase(Ease.OutQuad);
        if(buttons.Count>0)
            EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);// 비활성화 된 버튼을 동적으로 할당해준다.
    }

    public void ClosePanel()//버튼을 상호작용해 닫을 때 사용
    {
        panel.DOSizeDelta(new Vector2(originalSize.x, 0), closeDuration)
    .OnStart(() => StartCoroutine(UnsetButtons()))// 버튼 이미지 비활성화.
    .SetEase(Ease.InExpo)
    .OnComplete(() =>
        {
            GameManager.Instance.player.cantAction = false;//UI가 닫히면 플레이어의 움직임을 다시 허용
            GameManager.Instance.outerCover.SetActive(false);//UI가 닫히면 테두리도 비활성화
            this.gameObject.SetActive(false);
        });
    }
    public void ClosetPanel2()//start버튼을 눌러 닫을 때 사용
    {
        panel.DOSizeDelta(new Vector2(originalSize.x, 0), closeDuration)
    .OnStart(() => StartCoroutine(UnsetButtons()))// 버튼 이미지 비활성화.
    .SetEase(Ease.InExpo)
    .OnComplete(() => this.gameObject.SetActive(false));
    }

    IEnumerator SetButtons()
    {
        yield return new WaitForSeconds(openDuration);
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
    }// UI가 열리면(openDuration 만큼 대기) 버튼 표시.
    IEnumerator UnsetButtons()
    {
        yield return new WaitForSeconds(closeDuration * 0.5f);
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }// UI가 닫히면(closeDuration/2 만큼 대기) 버튼 숨김.
}
