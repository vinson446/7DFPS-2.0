using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GunManager : MonoBehaviour
{
    public event Action OnFire = delegate { };
    public event Action OnIdle = delegate { };
    public event Action OnReload = delegate { };

    [Header("Weapon GameObjs")]
    [SerializeField] GameObject[] weaponObjs;
    [SerializeField] GameObject[] ammoObjs;

    [Header("Shoot Settings")]
    float fireRate;
    [SerializeField] float bulletForce;
    [SerializeField] Transform rifleBulletSpawn;
    [SerializeField] Transform[] shotgunBulletSpawn;
    [SerializeField] Transform pistolBulletSpawn;
    GameObject bullet;
    bool isReloading;

    [Header("Rifle Settings")]
    [SerializeField] int currentRifleAmmo;
    [SerializeField] int rifleReloadAmt;
    [SerializeField] int maxRifleAmmo;
    public int MAXIMUMRIFLEAMMO;

    [Header("Shotgun Settings")]
    [SerializeField] int currentShotgunAmmo;
    [SerializeField] int shotgunReloadAmt;
    [SerializeField] int maxShotgunAmmo;
    public int MAXIMUMSHOTGUNAMMO;

    [Header("Pistol Settings")]
    [SerializeField] int currentPistolAmmo;
    [SerializeField] int pistolReloadAmt;

    [Header("FX")]
    [SerializeField] ParticleSystem[] muzzleFlashFX;
    bool showingFX;

    [Header("Animator")]
    Animator animator;
    public float rifleWaitTime;
    public float shotgunWaitTime;
    public float pistolWaitTime;

    [SerializeField] int currentWeapon;
    public int CurrentWeapon => currentWeapon;

    float shootCD;

    public float offsetX;
    public float offsetY;

    // references
    Player player;
    UIGameManager uiGameManager;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        uiGameManager = FindObjectOfType<UIGameManager>();

        animator = GetComponent<Animator>();

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
            CheckIfStartFiring();
            shootCD = Time.time + 1 / fireRate;
            Shoot();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            CheckIfStoppedFiring();
            ShowMuzzleFlash(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void ShowMuzzleFlash(bool yes)
    {
        if (yes)
        {
            if (!muzzleFlashFX[currentWeapon].gameObject.activeInHierarchy)
            {
                muzzleFlashFX[currentWeapon].gameObject.SetActive(true);
            }
        }
        else
        {
            if (muzzleFlashFX[currentWeapon].gameObject.activeInHierarchy)
            {
                muzzleFlashFX[currentWeapon].gameObject.SetActive(false);
            }
        }
    }

    void Shoot()
    {
        ShowMuzzleFlash(true);
        
        GameObject bullet = ammoObjs[0];

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
            // float x = Screen.width / 2;
            //float y = Screen.height / 2;
            // var ray = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

            switch (currentWeapon)
            {
                case 0:
                    bullet = Instantiate(ammoObjs[0], rifleBulletSpawn.position, rifleBulletSpawn.rotation);
                    Bullet b1 = bullet.GetComponent<Bullet>();
                    b1.Damage = player.RifleDamage;
                    b1.transform.localScale = new Vector3(player.BulletSize, player.BulletSize, player.BulletSize);

                    Rigidbody rb1 = bullet.GetComponent<Rigidbody>();
                    rb1.AddForce(bulletForce * ray.direction);
                    break;
                case 1:
                    foreach(Transform t in shotgunBulletSpawn)
                    {
                        bullet = Instantiate(ammoObjs[1], t.position, t.rotation);
                        Bullet b2 = bullet.GetComponent<Bullet>();
                        b2.Damage = player.ShotgunDamage;
                        b2.transform.localScale = new Vector3(player.BulletSize, player.BulletSize, player.BulletSize);

                        Rigidbody rb2 = bullet.GetComponent<Rigidbody>();
                        rb2.AddForce(bulletForce * t.transform.forward);
                    }
                    break;
                case 2:
                    bullet = Instantiate(ammoObjs[2], pistolBulletSpawn.position, pistolBulletSpawn.rotation);
                    Bullet b3 = bullet.GetComponent<Bullet>();
                    b3.Damage = player.PistolDamage;
                    b3.transform.localScale = new Vector3(player.BulletSize, player.BulletSize, player.BulletSize);

                    Rigidbody rb3 = bullet.GetComponent<Rigidbody>();
                    rb3.AddForce(bulletForce * ray.direction);
                    break;
            }

            UseBulletOnBackend();
        }
    }

    private void CheckIfStartFiring()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= shootCD && !isReloading)
        {
            OnFire?.Invoke();
        }
    }

    private void CheckIfStoppedFiring()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            OnIdle?.Invoke();
        }
    }

    private void CheckIfStartReload()
    {
        OnReload?.Invoke();
    }

    private void CheckIfStoppedReload()
    {
        OnIdle?.Invoke();
    }

    void Reload()
    {
        if (!isReloading)
            StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        ShowMuzzleFlash(false);

        CheckIfStartReload();
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
        CheckIfStoppedReload();
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
        foreach(GameObject o in weaponObjs)
        {
            o.SetActive(false);
        }
        weaponObjs[index].SetActive(true);

        currentWeapon = index;
        
        if (currentWeapon == 0)
        {
            fireRate = player.RifleFireRate;
        }
        else if (currentWeapon == 1)
        {
            fireRate = player.ShotgunFireRate;
        }
        else if (currentWeapon == 2)
        {
            fireRate = player.PistolFireRate;
        }

        UpdateAmmoUI();

        ChangeAnimator();

        uiGameManager.UpdateWeaponImage(currentWeapon);
    }

    void ChangeAnimator()
    {
        if (currentWeapon == 0)
        {
            animator.runtimeAnimatorController = Resources.Load("Rifle") as RuntimeAnimatorController;
        }
        else if (currentWeapon == 1)
        {
            animator.runtimeAnimatorController = Resources.Load("Shotgun") as RuntimeAnimatorController;
        }
        else if (currentWeapon == 2)
        {
            animator.runtimeAnimatorController = Resources.Load("Pistol") as RuntimeAnimatorController;
        }
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

    public void GainAmmoOnBackend(int rifleAmmoPickup, int shotgunAmmoPickup)
    {
        maxRifleAmmo += rifleAmmoPickup;
        if (maxRifleAmmo > MAXIMUMRIFLEAMMO)
            maxRifleAmmo = MAXIMUMRIFLEAMMO;

        maxShotgunAmmo += shotgunAmmoPickup;
        if (maxShotgunAmmo > MAXIMUMSHOTGUNAMMO)
            maxShotgunAmmo = MAXIMUMSHOTGUNAMMO;

        UpdateAmmoUI();
    }
}
