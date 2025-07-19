using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform firePoint; // 발사 지점
    public float bulletForce = 1000f; // 발사 힘

    public int maxAmmo = 30; // 보유 가능한 최대 총알 개수.
    public int currentAmmo; // 현재 내가 보유한 총알 개수.
    public ParticleSystem muzzleFlash; // 총알 발사 시 재생할 이펙트 객체
    public AudioSource fireSound; // 총알 발사 시 재생할 사운드 객체

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            Shoot();
        }
        if(Input.GetButtonDown("Reload"))
        {
            currentAmmo = maxAmmo;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce);

        if(muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        if(fireSound != null && fireSound.clip != null) //AudioClip
        {
            fireSound.Play();
        }

        Destroy(bullet, 1);

        --currentAmmo;
    }
}
