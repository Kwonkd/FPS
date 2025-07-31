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
    public TextMeshProUGUI stateText; // ป๓ลย UI
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
                if(Input.GetKeyDown(KeyCode.Y))
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
                if (Input.GetKeyDown(KeyCode.Y))
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
                Shoot();
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
