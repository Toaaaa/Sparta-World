using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlappyBirdManeger : MonoBehaviour
{
    public Flyer flyer;// 플레이어가 조종하는 캐릭터.
    public PipeSpawner pipeSpawner;// 파이프 생성.

    public bool StartGame = false;
    public int Score = 0;

    public GameObject ResultUI;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultScoreText;

    private void Start()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private void Update()
    {
        scoreText.text =  "SCORE : " + Score.ToString();
        resultScoreText.text = "SCORE : " + Score.ToString();
    }

    private void OnEnable()
    {
        //처음에는 playerSpaceToStart 텍스트 추가
    }
    private void OnDisable()
    {
        ResultUI.SetActive(false);
    }
    public void StopGame()// 게임 오버.
    {
        pipeSpawner.StopPipe();
        StartGame = false;
        ResultUI.SetActive(true);
    }


    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(1f);
        StartGame = true;
        Score = 0;
    }
}
