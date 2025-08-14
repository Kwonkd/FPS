using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(gameObject.CompareTag("HPItem"))
            {
                HPItem();
            }
            else if(gameObject.CompareTag("MagazineItem"))
            {
                MagazineItem();
            }
            Destroy(gameObject);
        }
    }

    void HPItem()
    {
        PlayerStatus ph = GameObject.FindObjectOfType<PlayerStatus>();
        ph.Heal(30);
    }
    void MagazineItem()
    {
        Gun gun = GameObject.FindObjectOfType<Gun>();
        if(gun != null)
        {
            gun.currentAmmo = 30;
            gun.UpdateAmmoUI(); // UI 업데이트
        }
        Weapon weapon = GameObject.FindObjectOfType<Weapon>();
        if(weapon != null)
        {
            weapon.getItem(30);
        }
    }
}
