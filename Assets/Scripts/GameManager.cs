using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject singleton = new GameObject(typeof(GameManager).ToString());
                    instance = singleton.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    public Player player;
    public FlappyBirdManeger flappyBirdManeger;
    public FallingStoneManager fallingStoneManager;   
    public LeaderBoard rankingGame1;
    public LeaderBoard rankingGame2;
    public GameObject outerCover;// 테두리

    [SerializeField] private GameStartUI gameStartUI;
    [SerializeField] private GameObject game1;
    [SerializeField] private GameObject game2;
    [SerializeField] private AudioSource bgm;




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }
    public void StartGame1()
    {
        outerCover.SetActive(true);
        bgm.Stop();
        game1.SetActive(true);
    }
    public void StartGame2()
    {
        outerCover.SetActive(true);
        bgm.Stop();
        game2.SetActive(true);
    }
    public void SetGameName(string name)
    {
        gameStartUI.gameName = name;
    }
    public void GameStartUIOn()
    {
        gameStartUI.gameObject.SetActive(true);
    }
    public void MainBGMOn()
    {
        bgm.Play();
    }
}
