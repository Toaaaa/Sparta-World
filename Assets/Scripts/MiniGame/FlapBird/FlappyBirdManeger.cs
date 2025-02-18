using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlappyBirdManeger : MonoBehaviour
{
    public Flyer flyer;// 플레이어가 조종하는 캐릭터.
    public PipeSpawner pipeSpawner;// 파이프 생성.

    public bool StartGame = false;
    bool WaitingToStart = true;
    public int Score = 0;

    public GameObject ResultUI;
    public GameObject PressSpaceToStart;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultScoreText;

    private void Update()
    {
        scoreText.text =  "SCORE : " + Score.ToString();
        resultScoreText.text = "SCORE : " + Score.ToString();
        if (WaitingToStart)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                WaitingToStart = false;
                PressSpaceToStart.SetActive(false);
                StartCoroutine(StartGameCoroutine());
            }
        }

    }

    private void OnEnable()
    {
        PressSpaceToStart.SetActive(true);
        ResultUI.SetActive(false);
        WaitingToStart = true;
        GameSet();
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
        GameManager.Instance.rankingGame1.SetScore(Score);
    }
    public void PlayAgain()
    {
        GameSet();
        ResultUI.SetActive(false);
        PressSpaceToStart.SetActive(true);
        WaitingToStart = true;
    }
    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        StartGame = true;
        Score = 0;
    }
    private void GameSet()
    {
        pipeSpawner.ResetPipe();
        flyer.ResetFlyer();
    }
}
