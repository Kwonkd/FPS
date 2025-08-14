using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Weapon : MonoBehaviour
{
    public int magCapacity = 30;
    public int reserveAmmo = 90;
    public int currentAmmo;

    public event Action<int, int> OnAmmoChanged;

    public int damage = 10; // 한 발 당 대미지
    public float range = 50.0f; //총알 사정거리
    public Camera fpsCamera; // 발사 카메라
    public ParticleSystem muzzleFlash; // 총구 이팩트
    public GameObject impactEffect; // 명중 이팩트
    public AudioSource fireSound;
    public float fireRate = 0.2f; // 연사 속도
    public float nextTimeToFire = 0.0f; // 다음 발사까지 걸리는 시간
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = magCapacity; // 초기 탄약 설정
        RaiseAmmoChanged();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) == true && Time.time > nextTimeToFire && currentAmmo > 0)
        {
            Shoot();
            nextTimeToFire = Time.time + fireRate;
        }

        if (Input.GetKeyDown(KeyCode.R) == true)
        {
            Reload();
        }
    }

    public bool Shoot()
    {
        if (currentAmmo<=0)
        {
            return false;
        }

        currentAmmo--;
        //실제 발사 처리
        // 총구 이팩트 출력
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // RayCast를 이용한 명중 판정
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            if (fireSound != null)
            {
                fireSound.Play();
            }
            // 대미지 처리
            if (hit.transform.CompareTag("Enemy"))
            {
                EnemyAI target = hit.transform.GetComponent<EnemyAI>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }

            // 명중 이팩트.
            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 1);
        }
        RaiseAmmoChanged();
        return true;
    }

    public void Reload()
    {
        int needed = magCapacity - currentAmmo; // 현재 탄약과 탄창 용량의 차이
        if (needed <= 0 || reserveAmmo <= 0)
        {
            return;
        }

        int toLoad = Mathf.Min(needed, reserveAmmo);
        currentAmmo += toLoad;
        reserveAmmo -= toLoad;
        RaiseAmmoChanged();
    }

    private void RaiseAmmoChanged()
    {
        if(OnAmmoChanged != null)
        {
            OnAmmoChanged.Invoke(currentAmmo, reserveAmmo);
        }
    }

    public void getItem(int amount)
    {
        reserveAmmo += amount;
        RaiseAmmoChanged();
    }
}
