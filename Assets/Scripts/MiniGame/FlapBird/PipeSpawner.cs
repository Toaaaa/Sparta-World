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
        Debug.Log("파이프 생성대기 5초");
        await UniTask.Delay(2000);
        for (int i = 0; i < pipeSets.Count; i++)
        {
            if (pipeSets[i].gameObject.activeSelf == false)
            {
                Debug.Log("파이프 생성");
                pipeSets[i].gameObject.SetActive(true);
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
}
