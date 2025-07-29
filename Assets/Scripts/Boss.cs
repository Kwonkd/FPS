using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    // Start is called before the first frame update
/*    void Start()
    {
        maxHp = 10;
        score = 50;
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void TakeDamage(int damage)
    {
        PlayerMovement player = GameObject.FindObjectOfType<PlayerMovement>();
        Renderer renderer = GetComponent<Renderer>();
        currentHp -= damage;
        if(currentHp < 7 && currentHp != 0)
        {
            renderer.material.color = Color.red; // 체력이 7이하로 떨어지면 빨간색으로 변경
        }
        if (currentHp <= 0)
        {
            player.score += score;
            ProcessDead();
            Destroy(gameObject);
        }
    }*/
}
