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
        PlayerHealth ph = GameObject.FindObjectOfType<PlayerHealth>();
        if (ph != null)
        {
            if (ph.hp + 10 > ph.maxHp)
            {
                ph.hp = ph.maxHp;
            }
            else
            {
                ph.hp += 10;
            }
        }
    }
    void MagazineItem()
    {
        Gun gun = GameObject.FindObjectOfType<Gun>();
        if(gun != null)
        {
            gun.currentAmmo = 30;
            gun.UpdateAmmoUI(); // UI 업데이트
        }
    }
}
