using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdManeger : MonoBehaviour
{
    public Flyer flyer;// 플레이어가 조종하는 캐릭터.
    public bool StartGame = false;

    private void Start()
    {
        StartCoroutine(StartGameCoroutine());
    }
    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(1f);
        StartGame = true;
    }
}
