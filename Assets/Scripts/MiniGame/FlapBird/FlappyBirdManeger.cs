using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdManeger : MonoBehaviour
{
    public Flyer flyer;// 플레이어가 조종하는 캐릭터.
    public PipeSpawner pipeSpawner;// 파이프 생성.

    public bool StartGame = false;
    public int Score = 0;

    private void Start()
    {
        StartCoroutine(StartGameCoroutine());
    }

    public void StopGame()// 게임 오버.
    {
        pipeSpawner.StopPipe();
        StartGame = false;
    }


    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(1f);
        StartGame = true;
        Score = 0;
    }
}
