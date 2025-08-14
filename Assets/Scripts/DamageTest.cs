using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour
{
    public PlayerStatus player;
    public Weapon weapon;

    private void Awake()
    {
        if(player == null)
        {
            player = FindObjectOfType<PlayerStatus>();
        }

        if(weapon == null)
        {
            weapon = FindObjectOfType<Weapon>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H) == true)
        {
            player.TakeDamage(10.0f);
        }

        if (Input.GetKeyDown(KeyCode.J) == true)
        {
            player.Heal(10.0f);
        }

        if (Input.GetMouseButton(0) == true)
        {
            weapon.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.K) == true)
        {
            weapon.Reload();
        }
    }
}
