using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public Animator animator;
    // Start is called before the first frame update
    public int maxHp = 3;
    public int currentHp;
    public int score = 10;
    void Start()
    {
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dead()
    {
        //animator.SetBool("dead", true);
    }
    public void ProcessDead()
    {
        WaveManager manager = GameObject.FindObjectOfType<WaveManager>();
        if (manager != null)
        {
            manager.OnEnemyDefeated();
        }
    }
    virtual public void TakeDamage(int damage)
    {
        PlayerMovement player = GameObject.FindObjectOfType<PlayerMovement>();
        currentHp -= damage;
        if (currentHp <= 0)
        {
            player.score += score;
            ProcessDead();
            Destroy(gameObject);
        }
    }
}
