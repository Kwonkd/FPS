using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Bullet : MonoBehaviour
{
    public ParticleSystem prefab_hitEffect;
    public int damage = 1;
    public float shootPower = 10f;

    Rigidbody rb;
    //    public float shootPower = 5.0f; // 총알이 날라가는 속도 결정
    // Start is called before the first frame update
    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Shoot();
        Destroy(gameObject, 1.5f);
    }

    void Shoot()
    {
        rb.AddForce(transform.forward * shootPower, ForceMode.Impulse); // 총알이 힘을 받아 발사
        Destroy(gameObject, 1); // 총알이 발사된 후 1초뒤에 파괴
    }
    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.tag == "Target") //태그가 타켓인 게임오브젝트에 충돌 시 충돌한 게임오브젝트와 총알 파괴
        {
            ParticleSystem hitEffect = Instantiate(prefab_hitEffect, collision.gameObject.transform.position, Quaternion.identity);

            hitEffect.Play();
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Destroy(hitEffect,1);
        }*/
        //if(collision.gameObject.tag =="Enemy")
        //{
            //EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
            //if (enemy != null)
            //{
                //enemy.Dead();
                //enemy.ProcessDead();
                //Destroy(gameObject);
                //Destroy(enemy.gameObject);
                //gameObject.SetActive(false);
                //enemy.TakeDamage(damage);
            //}
        //}
       //else // 태그가 타겟이 아닌 오브젝트에 충돌시 총알 파괴
        //{
            //Invoke("SetActiveBullet", 1);
            //Destroy(gameObject); 
        //}

        if(collision.gameObject.tag == "Player")
        {
            PlayerStatus ph = collision.gameObject.GetComponent<PlayerStatus>();
            //PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
                Debug.Log("Enemy attacked player for " + damage + " damage.");
                Destroy(gameObject);
            }
        }
        //else // 태그가 타겟이 아닌 오브젝트에 충돌시 총알 파괴
        //{
           // Invoke("SetActiveBullet", 1);
            //Destroy(gameObject); 
        //}
    }

    void SetActiveBullet()
    {
        gameObject.SetActive(false);
    }
}
