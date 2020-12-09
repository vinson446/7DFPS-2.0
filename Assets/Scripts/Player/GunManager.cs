using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [Header("Shoot Settings")]
    [SerializeField] GameObject[] bulletObjs;
    [SerializeField] float bulletForce;
    [SerializeField] Transform bulletSpawn;
    GameObject bullet;
    bool isReloading;

    [Header("Rifle Settings")]
    [SerializeField] int currentRifleAmmo;
    [SerializeField] int rifleReloadAmt;
    [SerializeField] int maxRifleAmmo;

    [Header("Shotgun Settings")]
    [SerializeField] int currentShotgunAmmo;
    [SerializeField] int shotgunReloadAmt;
    [SerializeField] int maxShotgunAmmo;

    [Header("Pistol Settings")]
    [SerializeField] int currentPistolAmmo;
    [SerializeField] int pistolReloadAmt;

    [Header("Pickup Settings")]
    [SerializeField] int riflePickupAmt;
    [SerializeField] int shotgunPickupAmt;
    [SerializeField] int pistolPickupAmt;

    int currentWeapon;
    float shootCD;

    // references
    Player player;
    UIGameManager uiGameManager;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        uiGameManager = FindObjectOfType<UIGameManager>();

        currentRifleAmmo = rifleReloadAmt;
        currentShotgunAmmo = shotgunReloadAmt;
        currentPistolAmmo = pistolReloadAmt;

        ChangeWeaponOnBackend(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeaponOnBackend(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeaponOnBackend(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeaponOnBackend(2);
        }

        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= shootCD && !isReloading)
        {
            shootCD = Time.time + 1 / player.FireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
        GameObject bullet = bulletObjs[0];

        if (currentWeapon == 0 && currentRifleAmmo <= 0)
        {
            Reload();
        }
        else if (currentWeapon == 1 && currentShotgunAmmo <= 0)
        {
            Reload();
        }
        else if (currentWeapon == 2 && currentPistolAmmo <= 0)
        {
            Reload();
        }
        else
        {
            switch (currentWeapon)
            {
                case 0:
                    bullet = Instantiate(bulletObjs[0], bulletSpawn.position, bulletSpawn.rotation);
                    break;
                case 1:
                    bullet = Instantiate(bulletObjs[1], bulletSpawn.position, bulletSpawn.rotation);
                    break;
                case 2:
                    bullet = Instantiate(bulletObjs[2], bulletSpawn.position, bulletSpawn.rotation);
                    break;
            }

            float x = Screen.width / 2;
            float y = Screen.height / 2;

            var ray = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(bulletForce * ray.direction);

            UseBulletOnBackend();
        }
    }

    void Reload()
    {
        if (!isReloading)
            StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        isReloading = true;

        yield return new WaitForSeconds(1f);

        switch (currentWeapon)
        {
            case 0:
                if (maxRifleAmmo >= rifleReloadAmt - currentRifleAmmo)
                {
                    maxRifleAmmo -= rifleReloadAmt - currentRifleAmmo;
                    currentRifleAmmo = rifleReloadAmt;
                }
                else
                {
                    currentRifleAmmo = maxRifleAmmo;
                    maxRifleAmmo = 0;
                }
                break;
            case 1:
                if (maxShotgunAmmo >= shotgunReloadAmt - currentShotgunAmmo)
                {
                    maxShotgunAmmo -= shotgunReloadAmt - currentShotgunAmmo;
                    currentShotgunAmmo = shotgunReloadAmt;
                }
                else
                {
                    currentShotgunAmmo = maxShotgunAmmo;
                    maxShotgunAmmo = 0;
                }
                break;
            case 2:
                currentPistolAmmo = pistolReloadAmt;
                break;
        }

        UpdateAmmoUI();

        isReloading = false;
    }

    void UpdateAmmoUI()
    {
        switch (currentWeapon)
        {
            case 0:
                uiGameManager.UpdateAmmo(currentRifleAmmo, maxRifleAmmo);
                break;
            case 1:
                uiGameManager.UpdateAmmo(currentShotgunAmmo, maxShotgunAmmo);
                break;
            case 2:
                uiGameManager.UpdateAmmo(currentPistolAmmo, Mathf.Infinity);
                break;
        }
    }

    public void ChangeWeaponOnBackend(int index)
    {
        currentWeapon = index;

        UpdateAmmoUI();
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

        UpdateAmmoUI();
    }

    public void GainAmmoOnBackend()
    {
        currentRifleAmmo += riflePickupAmt;
        if (currentRifleAmmo > maxRifleAmmo)
            currentRifleAmmo = maxRifleAmmo;

        currentShotgunAmmo += shotgunPickupAmt;
        if (currentShotgunAmmo > maxShotgunAmmo)
            currentShotgunAmmo = maxShotgunAmmo;

        UpdateAmmoUI();
    }
}
