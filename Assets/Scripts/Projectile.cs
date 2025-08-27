using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    private void Start()
    {
        Destroy(gameObject, 3f);
    }
    private void Update()
    {
        Rigidbody bulletRb = gameObject.GetComponent<Rigidbody>();
        if (bulletRb != null) bulletRb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerStatus ps = other.gameObject.GetComponent<PlayerStatus>();
        if (other.gameObject.tag != "Enemy")
        {
            if (ps != null)
            {
                ps.TakeDamage(damage);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
