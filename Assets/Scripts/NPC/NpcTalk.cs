using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;

public class NpcTalk : MonoBehaviour
{
    public List<string> talkData = new List<string>()
    {
        "집밖으로 나가기 싫어...",
        "밖은 무서워...",
        "그냥 집에 있으면 안될까..?",
        ".......",
    };
    public GameObject talkPanel;
    public TextMeshPro talkText;

    CancellationTokenSource cts;// 대화를 한번더 호출시 기존의 비활성화 호출을 취소하기 위한 변수.

    public void Talk()
    {
        if(cts != null)
        {
            cts.Cancel();
            cts.Dispose();
        }// 기존의 토큰이 있다면 취소
        cts = new CancellationTokenSource();

        UniTask.Void(async () =>
        {
            talkPanel.SetActive(true);
            talkText.text = talkData[Random.Range(0, talkData.Count)];
            await UniTask.Delay(3500,cancellationToken : cts.Token);
            talkPanel.SetActive(false);
        });
    }
}
