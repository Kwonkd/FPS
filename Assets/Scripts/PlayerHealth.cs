using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHp = 100; // 최대 체력
    public int hp;
    public GameObject gameOverUI;

    void Start()
    {
        hp = maxHp; // 시작 시 최대 체력으로 초기화
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log("플레이어가 대미지를 입었습니다. 현재 HP : " + hp);

        if(hp <= 0)
        {
            //게임 오버 처리. 씬 전환, 게임 오버 UI 출력
            Debug.Log("플레이어 사망");
            Cursor.lockState = CursorLockMode.None;
            gameOverUI.SetActive(true);
            gameObject.GetComponent<PlayerMovement>().enabled = false; // 플레이어의 체력이 0 이하가 되면 PlayerMovement 스크립트 비활성화, 총은 쏴지기 때문에 의미는 없음..
        }
    }
}
