using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    protected override void Start()
    {
        maxAmmo = 10;
        fireRate = 0.4f;
        damage = 15;
        range = 20f;

        base.Start();
    }

    void Update()
    {
        
    }
}
