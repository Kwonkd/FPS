using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int hp = 100;
    public GameObject gameOverUI;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log("플레이어가 대미지를 입었습니다. 현재 HP : " + hp);

        if(hp <= 0)
        {
            Debug.Log("플레이어 사망");
            Cursor.lockState = CursorLockMode.None;
            gameOverUI.SetActive(true);
            //게임 오버 처리. 씬 전환, 게임 오버 UI 출력
        }
    }
}
