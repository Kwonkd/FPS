using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public enum GunState
{
    auto,
    semiauto
}
public class Rifle : Gun
{
    public GunState gunstate = GunState.auto;
    public float semiautoFireRate = 1.0f;
    public TextMeshProUGUI stateText; // 상태 UI
    // Start is called before the first frame update
    protected override void Start()
    {
        maxAmmo = 30;
        damage = 10;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAmmoUI();
        switch (gunstate)
        {
            case GunState.auto:
                if(Input.GetButton("Fire1")&& Time.time > nextTimeToFire && currentAmmo > 0)
                {
                    nextTimeToFire = Time.time + fireRate;
                    Shoot();
                }
                if(Input.GetButtonDown("ModeChange"))
                {
                    gunstate = GunState.semiauto;
                }
                break;
            case GunState.semiauto:
                if (Input.GetButton("Fire1") && Time.time > nextTimeToFire && currentAmmo > 0)
                {
                    nextTimeToFire = Time.time + semiautoFireRate;
                    Semiauto();
                }
                if (Input.GetButtonDown("ModeChange"))
                {
                    gunstate = GunState.auto;
                }
                break;
        }
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    void Semiauto()
    {
        if (currentAmmo >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                --currentAmmo; // 탄약 감소
                UpdateAmmoUI();

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
            }
        }
    }

    override public void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            stateText.text = gunstate.ToString();
            ammoText.text = currentAmmo + " / " + maxAmmo;
        }
    }
}
