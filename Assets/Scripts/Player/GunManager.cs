using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [Header("Rifle Settings")]
    [SerializeField] int currentRifleAmmo;
    [SerializeField] int maxRifleAmmo;

    [Header("Shotgun Settings")]
    [SerializeField] int currentShotgunAmmo;
    [SerializeField] int maxShotgunAmmo;

    [Header("Pistol Settings")]
    [SerializeField] int currentPistolAmmo;
    [SerializeField] int maxPistolAmmo;

    [Header("Pickup Settings")]
    [SerializeField] int riflePickupAmt;
    [SerializeField] int shotgunPickupAmt;
    [SerializeField] int pistolPickupAmt;

    int currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeWeaponOnBackend(int index)
    {
        currentWeapon = index;
    }

    public void UseBulletOnBackend()
    {
        switch (currentWeapon)
        {
            case 0:
                currentRifleAmmo -= 1;
                break;
            case 1:
                currentShotgunAmmo -= 1;
                break;
            case 2:
                currentPistolAmmo -= 1;
                break;
        }
    }

    public void GainWeaponOnBackend()
    {
        currentRifleAmmo += riflePickupAmt;
        if (currentRifleAmmo > maxRifleAmmo)
            currentRifleAmmo = maxRifleAmmo;

        currentShotgunAmmo += shotgunPickupAmt;
        if (currentShotgunAmmo > maxShotgunAmmo)
            currentShotgunAmmo = maxShotgunAmmo;

        currentPistolAmmo += pistolPickupAmt;
        if (currentPistolAmmo > maxPistolAmmo)
            currentPistolAmmo = maxPistolAmmo;
    }
}
