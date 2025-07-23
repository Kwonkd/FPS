using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
