using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonSelector : MonoBehaviour
{
    public Button firstButton;
    public float scaleFactor = 1.2f; // 버튼이 커질 크기
    private GameObject lastSelected; // 이전에 선택된 버튼 저장

    private void OnEnable()
    {
        if (firstButton != null)
        {
            EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
            lastSelected = firstButton.gameObject;
            ScaleButton(lastSelected, scaleFactor); // 첫번째 버튼 크기 증가
        }
    }

    void Update()
    {
        GameObject selectedObj = EventSystem.current.currentSelectedGameObject;

        if (selectedObj != null && selectedObj != lastSelected)
        {
            ResetLastButtonSize(); // 이전 버튼 크기 원래대로
            ScaleButton(selectedObj, scaleFactor); // 현재 버튼 크기 증가
            lastSelected = selectedObj;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (selectedObj != null)
            {
                Button btn = selectedObj.GetComponent<Button>();
                btn?.onClick.Invoke(); // 현재 선택된 버튼 실행
            }
        }
    }

    void ResetLastButtonSize()
    {
        if (lastSelected != null)
        {
            ScaleButton(lastSelected, 1f); // 크기 원래대로 돌리기
        }
    }

    void ScaleButton(GameObject buttonObj, float scale)
    {
        RectTransform rect = buttonObj.GetComponent<RectTransform>();
        if (rect != null)
        {
            rect.localScale = Vector3.one * scale; // 크기 변경
        }
    }
}
