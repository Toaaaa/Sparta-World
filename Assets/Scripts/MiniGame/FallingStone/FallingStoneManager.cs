using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FallingStoneManager : MonoBehaviour
{
    public FallingStonePlayer player;
    public StoneSpawner stoneSpawner;

    public bool StartGame = false;
    bool WaitingToStart = true;
    public int Score = 0;

    public GameObject ResultUI;
    public GameObject PressSpaceToStart;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultScoreText;
    
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
    private void Update()
    {
        scoreText.text = "SCORE : " + Score.ToString();
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
    public void StopGame()// 게임 오버.
    {
        stoneSpawner.StopStone();
        StartGame = false;
        ResultUI.SetActive(true);
        GameManager.Instance.rankingGame2.SetScore(Score);
    }
    public void PlayAgain()
    {
        GameSet();
        ResultUI.SetActive(false);
        PressSpaceToStart.SetActive(true);
        WaitingToStart = true;
    }
    private void GameSet()
    {
        player.ResetPlayer();
        stoneSpawner.ResetStone();
        Score = 0;
    }
    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        StartGame = true;
        stoneSpawner.StartSpawn();
    }
}
