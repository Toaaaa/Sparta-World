using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class PipeSpawner : MonoBehaviour
{
    public List<PipeSet> pipeSets;
    bool isSpawning = false;//파이프 생성중에는 추가 생성 멈춤.

    private void Update()
    {
        if (GameManager.Instance.flappyBirdManeger.StartGame)
        {
            _ = SetPipes();
        }
    }
    
    async UniTask SetPipes()
    {
        if (isSpawning)
            return;
        isSpawning = true;
        await UniTask.Delay(2000);
        if(!GameManager.Instance.flappyBirdManeger.StartGame)// 게임 종료시 파이프 생성 정지.
        {
            isSpawning = false;
            return;
        }
        for (int i = 0; i < pipeSets.Count; i++)
        {
            if (pipeSets[i].gameObject.activeSelf == false)
            {
                pipeSets[i].gameObject.SetActive(true);// 파이프 생성
                pipeSets[i].SetPipe();
                pipeSets[i].StartMoving();
                isSpawning = false;
                break;
            }
        }

    }
    public void StopPipe()
    {
        for(int i = 0; i < pipeSets.Count; i++)
        {
            if (pipeSets[i].gameObject.activeSelf)
                    pipeSets[i].StopMoving();
        }
    }
    public void ResetPipe()
    {
        for (int i = 0; i < pipeSets.Count; i++)
        {
            if (pipeSets[i].gameObject.activeSelf)
            {
                pipeSets[i].gameObject.SetActive(false);
                pipeSets[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);// 위치 초기화.
            }
        }
    }
}
