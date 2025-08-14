using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float maxHP = 100f; // 플레이어의 최대 체력
    private float currentHP; // 현재 체력

    public event Action<float, float> OnHpChanged;
    public event Action OnDied;

    public float CurrentHP => currentHP;

    public GameObject gameOverUI;
    public bool IsDead // set은 허용하지 않고 get만 허용
    {
        get;
        private set;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP; // 초기 체력 설정
        RaiseHpChanged(); // 초기 체력 변경 이벤트 호출
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float amount)
    {
        if (IsDead == true)
        {
            return; // 이미 죽은 상태라면 아무것도 하지 않음
        }

        currentHP = Mathf.Max(0, currentHP - amount); // 0과 비교하여 더 큰 값을 비교해서 가져온다. 최소 체력을 0으로 설정
        RaiseHpChanged(); // 체력 변경 이벤트 호출

        if (currentHP <= 0)
        {
            IsDead = true;
            Cursor.lockState = CursorLockMode.None;
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
            //gameObject.GetComponent<PlayerMovement>().enabled = false; // 플레이어의 체력이 0 이하가 되면 PlayerMovement 스크립트 비활성화, 총은 쏴지기 때문에 의미는 없음..
            if (OnDied != null)
            {
                // 플레이어가 죽었을 때 이벤트 호출
                OnDied.Invoke();
            }
        }
    }

    public void Heal(float amount)
    {
        if (IsDead == true)
        {
            return; // 이미 죽은 상태라면 아무것도 하지 않음
        }

        currentHP = Mathf.Min(maxHP, currentHP + amount); // 최대 체력과 비교하여 더 작은 값을 설정
        RaiseHpChanged();
    }
    private void RaiseHpChanged()
    {
        if (OnHpChanged != null)
        {
            // 이벤트 함수 호출.
            OnHpChanged.Invoke(currentHP, maxHP);
        }
    }
}
