using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    public TextMeshProUGUI highscore; //1~3위까지의 순위를 저장할 GameObject 배열

    private void Start()
    {
        //랭킹 정보를 불러와서 Text에 저장
        if(PlayerPrefs.HasKey(gameObject.name))
            highscore.text = PlayerPrefs.GetString(gameObject.name);
        else
            highscore.text = "0";
    }

    public void SetScore(int score)// 더 높은 점수 달성시 하이스코어 갱신.
    {
        if(score > int.Parse(highscore.text))
        {
            highscore.text = score.ToString();
            PlayerPrefs.SetString(gameObject.name, score.ToString());
        }
    }
}
