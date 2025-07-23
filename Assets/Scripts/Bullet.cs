using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem prefab_hitEffect;
//    public float shootPower = 5.0f; // 총알이 날라가는 속도 결정
//    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
//        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
//        Shoot();
    }

   /* void Shoot()
    {
        rb.AddForce(transform.forward * shootPower); // 총알이 힘을 받아 발사
        Destroy(gameObject, 1); // 총알이 발사된 후 1초뒤에 파괴
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target") //태그가 타켓인 게임오브젝트에 충돌 시 충돌한 게임오브젝트와 총알 파괴
        {
            ParticleSystem hitEffect = Instantiate(prefab_hitEffect, collision.gameObject.transform.position, Quaternion.identity);

            hitEffect.Play();
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Destroy(hitEffect,1);
        }
        if(collision.gameObject.tag =="Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                //enemy.Dead();
                enemy.ProcessDead();
                Destroy(gameObject);
                Destroy(enemy.gameObject);
            }
        }
       /*else // 태그가 타겟이 아닌 오브젝트에 충돌시 총알 파괴
        {
            Destroy(gameObject); 
        }*/
    }
}
