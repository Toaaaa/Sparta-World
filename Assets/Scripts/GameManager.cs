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


    [SerializeField] private GameStartUI gameStartUI;
    [SerializeField] private GameObject game1;
    [SerializeField] private GameObject game2;



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
        game1.SetActive(true);
    }
    public void StartGame2()
    {
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
}
