using UnityEngine;

/// <summary>
/// Handles player combat: shooting, reloading, and melee attacks.
/// Attach this to the Player.
/// </summary>
public class PlayerCombat : MonoBehaviour
{
    [Header("Gun Settings")]
    public int maxAmmo = 30;
    public int currentAmmo;
    public float fireRate = 0.2f;
    public float reloadTime = 2f;
    public float range = 100f;
    public float damage = 25f;

    [Header("References")]
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animator animator;

    [Header("Melee Settings")]
    public float meleeDamage = 40f;
    public float meleeRange = 2f;

    private float nextTimeToFire = 0f;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
        UIManager.instance.UpdateAmmo(currentAmmo, maxAmmo);
    }

    void Update()
    {
        if (isReloading) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            MeleeAttack();
        }

        // Low ammo warning
        if (currentAmmo <= 3 && currentAmmo > 0)
        {
            UIManager.instance.LowAmmoWarning();
        }
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            UIManager.instance.LowAmmoWarning();
            return;
        }

        currentAmmo--;
        UIManager.instance.UpdateAmmo(currentAmmo, maxAmmo);

        if (muzzleFlash) muzzleFlash.Play();
        if (animator) animator.SetTrigger("Shoot");

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            ZombieHealth zombie = hit.transform.GetComponent<ZombieHealth>();
            if (zombie != null)
            {
                float finalDamage = ZombieDifficultySystem.GetZombieDamage(damage);
                zombie.TakeDamage(finalDamage);
                UIManager.instance.ShowHitMarker();
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);
        }
    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        if (animator) animator.SetTrigger("Reload");
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        UIManager.instance.UpdateAmmo(currentAmmo, maxAmmo);
        isReloading = false;
    }

    void MeleeAttack()
    {
        Debug.Log("Player melee attack!");
        if (animator) animator.SetTrigger("Melee");

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, meleeRange))
        {
            ZombieHealth zombie = hit.transform.GetComponent<ZombieHealth>();
            if (zombie != null)
            {
                zombie.TakeDamage(meleeDamage);
                UIManager.instance.ShowHitMarker();
            }
        }
    }
}
