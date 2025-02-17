using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    public string gameName; //game1 or game2


    public void StartGame()
    {
        switch (gameName)
        {
            case "Game1":
                GameManager.Instance.StartGame1();
                break;
            case "Game2":
                GameManager.Instance.StartGame2();
                break;
            default:
                Debug.Log("게임이 입력되지 않았습니다");
                break;
        }
        this.gameObject.SetActive(false);// 게임 시작후 ui 비활성화.
    }

}
