using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 3f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        PlayerStatus ps = collision.gameObject.GetComponent<PlayerStatus>();
        if (collision.gameObject.tag != "Enemy")
        {
            if (ps != null)
            {
                ps.TakeDamage(15);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
