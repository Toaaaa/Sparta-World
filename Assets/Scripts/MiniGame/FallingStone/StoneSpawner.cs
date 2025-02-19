using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class StoneSpawner : MonoBehaviour
{
    public List<GameObject> stoneList;
    public GameObject stonePrefab;
    bool isSpawning = false;// 돌 생성중에는 추가 생성 멈춤.

    private void Update()
    {
        if (GameManager.Instance.fallingStoneManager.StartGame)
        {
            _ = SetStones();
        }
    }

    async UniTask SetStones()
    {
        if (isSpawning)
            return;
        isSpawning = true;
        await UniTask.Delay(2000);
        if (!GameManager.Instance.fallingStoneManager.StartGame)// 게임 종료시 돌 생성 정지.
        {
            isSpawning = false;
            return;
        }
        for (int i = 0; i < stoneList.Count; i++)
        {
            if (stoneList[i].activeSelf == false)
            {
                stoneList[i].SetActive(true);// 돌 생성
                StonePlace(stoneList[i]);
                isSpawning = false;
                break;
            }
        }
    }
    void SpawnStone()
    {
        if (CheckAllActive())
        {
            GameObject stone = Instantiate(stonePrefab, transform.position, Quaternion.identity);
            stoneList.Add(stone);// 새로운 돌 생성 + 추가.
        }
        else
        {
            foreach (GameObject stone in stoneList)
            {
                if (stone.activeSelf == false)
                {
                    StonePlace(stone);// 돌의 위치 조정.
                    stone.SetActive(true);
                    break;
                }
            }
        }
    }

    bool CheckAllActive()
    {
        foreach(GameObject stone in stoneList)
        {
            if(stone.activeSelf == false)
            {
                return false;
            }
        }
        return true;
    }// 만약 모든 돌이 활성화 상태라면.

    public void StonePlace(GameObject stone)
    {
        // -660 ~ 660 의 범위
        float x = Random.Range(-660, 660);
        stone.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 0);
    }
    public void ResetStone()
    {

    }
    public void StopStone()
    {
        
    }
}
