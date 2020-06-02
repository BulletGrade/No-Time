using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSystem : MonoBehaviour
{
    // For whoever is reading my code, be aware this isn't well optimized
    // This is my first time scripting a gun system, so it's of course a mess

    public Camera playerCamera;
    public GameObject bullet;
    public GameObject shell;
    public Image crosshair;
    public Text guide;
    public Text ammoCount;
    public Collider playerCollider;
    public float fireRate = 0.16f;
    public float bulletSpeed = 1500f;
    public float offset = 2f;
    public bool hasPistol = false;
    public bool hasSMG = false;
    public bool hasShotgun = false;
    public string heldWeapon = null;
    private float lastFired = 0f;
    private int pistolAmmo = 12;
    private int smgAmmo = 25;
    private int shotgunAmmo = 2;
    private bool isReloading = false;
    private bool isSwapping = false;
    private GameObject firingBullet;

    [Header("Sprites")]
    public Sprite regularCrosshair;
    public Sprite shotgunCrosshair;

    [Header("Sounds")]
    public AudioSource gunFire;
    public AudioSource sfx;
    public AudioClip pistolFire;
    public AudioClip smgFire;
    public AudioClip shotgunFire;
    public AudioClip reloadingPistolSound;
    public AudioClip reloadingSMGSound;
    public AudioClip reloadingShotgunSound;
    
    [Header("Animations")]
    public Animator pistolAnimation;
    public Animator smgAnimation;
    public Animator shotgunAnimation;

    void Update()
    {
        // Swap to Pistol
        if (Input.GetKeyDown(KeyCode.Alpha1) && hasPistol == true && !isSwapping)
        {
            if (heldWeapon != null)
            {
                StartCoroutine("WeaponSwapping");
            }
            else
            {
                transform.GetChild(0).GetComponent<Renderer>().enabled = true;
            }
            heldWeapon = "Pistol";
            crosshair.color = Color.white;
            crosshair.sprite = regularCrosshair;
            gunFire.clip = pistolFire;
        }
        // Swap to SMG
        if (Input.GetKeyDown(KeyCode.Alpha2) && hasSMG == true && !isSwapping)
        {
            if (heldWeapon != null)
            {
                StartCoroutine("WeaponSwapping");
            }
            else
            {
                transform.GetChild(1).GetComponent<Renderer>().enabled = true;
            }
            heldWeapon = "SMG";
            crosshair.color = Color.white;
            crosshair.sprite = regularCrosshair;
            gunFire.clip = smgFire;
        }
        // Swap to Shotgun
        if (Input.GetKeyDown(KeyCode.Alpha3) && hasShotgun == true && !isSwapping)
        {
            if (heldWeapon != null)
            {
                StartCoroutine("WeaponSwapping");
            }
            else
            {
                transform.GetChild(2).GetComponent<Renderer>().enabled = true;
            }
            heldWeapon = "Shotgun";
             crosshair.color = Color.white;
            crosshair.sprite = shotgunCrosshair;
            gunFire.clip = shotgunFire;
        }



        // Fire
        if (Input.GetKeyDown(KeyCode.Mouse0) && lastFired <= Time.time && heldWeapon == "Pistol" || Input.GetKeyDown(KeyCode.Mouse0) && heldWeapon == "Shotgun" && lastFired + 0.4f <= Time.time)
        {
            if (heldWeapon == "Pistol" && pistolAmmo <= 0 || heldWeapon == "SMG" && smgAmmo <= 0 || heldWeapon == "Shotgun" && shotgunAmmo <= 0)
            {
                guide.text = ("No ammo");
            }
            else
            {
                Fire();
                lastFired = Time.time + fireRate;
            }
        }
        // Hold Fire if holding SMG
        else if (Input.GetKey(KeyCode.Mouse0) && lastFired <= Time.time && heldWeapon == "SMG")
        {
            if (smgAmmo <= 0)
            {
                guide.text = ("No ammo");
            }
            else
            {
                Fire();
                lastFired = Time.time + fireRate;
            }
        }

        // Count how many ammo player has
        AmmoCounter();

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            {
                if (heldWeapon == "Pistol" && pistolAmmo <= 11 || heldWeapon == "SMG" && smgAmmo <= 24 || heldWeapon == "Shotgun" && shotgunAmmo <= 1)
                {
                    StartCoroutine("Reload");
                }
            }

        transform.rotation = Quaternion.Lerp(transform.rotation, playerCamera.transform.rotation, 5 * Time.deltaTime);
    }


    void Fire()
    {
        if (heldWeapon == "Pistol" && Time.timeScale > 0.1 || heldWeapon == "SMG" && Time.timeScale > 0.1) // For Pistol or SMG
        {
            switch (heldWeapon)
            {
                case "Pistol":
                    pistolAnimation.SetTrigger("Shooting");
                    pistolAmmo--;
                    break;
                case "SMG":
                    smgAnimation.SetTrigger("Shooting");
                    smgAmmo--;
                    break;
                default:
                    Debug.LogError("I don't know what you are shooting with");
                    break;
            }
            gunFire.Play();
            Vector3 spread = playerCamera.transform.up * Random.Range(-0.05f, 0.05f); // Vertical spread
            spread += playerCamera.transform.right * Random.Range(-0.05f, 0.05f); // Horizontal spread
            Quaternion spreadAngle = Quaternion.AngleAxis(0f, playerCamera.transform.forward + spread);
            firingBullet = Instantiate(bullet, playerCamera.transform.position + playerCamera.transform.forward + spread * offset, playerCamera.transform.rotation);
            Physics.IgnoreCollision(playerCollider, firingBullet.GetComponent<Collider>(), true);
            firingBullet.transform.forward = playerCamera.transform.forward + spread;
            firingBullet.GetComponent<Rigidbody>().velocity = playerCamera.transform.forward * bulletSpeed * Time.deltaTime;
            Recoil();
        }
        else if (heldWeapon == "Shotgun" && Time.timeScale > 0.1) // For shotgun
        {
            shotgunAnimation.SetTrigger("Shooting");
            shotgunAmmo--;
            gunFire.Play();
            for (int i = 0; i < 8; i++)
            {
                Vector3 spread = playerCamera.transform.up * Random.Range(-0.2f, 0.2f); // Vertical spread
                spread += playerCamera.transform.right * Random.Range(-0.2f, 0.2f); // Horizontal spread
                Quaternion spreadAngle = Quaternion.AngleAxis(0f, playerCamera.transform.forward + spread);
                firingBullet = Instantiate(shell, playerCamera.transform.position + playerCamera.transform.forward + spread * offset, playerCamera.transform.rotation);
                Physics.IgnoreCollision(playerCollider, firingBullet.GetComponent<Collider>(), true);
                firingBullet.transform.forward = playerCamera.transform.forward + spread;
                firingBullet.GetComponent<Rigidbody>().velocity = playerCamera.transform.forward * bulletSpeed * Time.deltaTime;
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        switch (heldWeapon)
            {
                case "Pistol":
                    lastFired = Time.time + 2.3f;
                    sfx.clip = reloadingPistolSound;
                    sfx.Play();
                    pistolAnimation.SetTrigger("Reloading");
                    yield return new WaitForSeconds(1.5f);
                    pistolAmmo = 12;
                    isReloading = false;
                    break;
                case "SMG":
                    lastFired = Time.time + 2.5f;
                    sfx.clip = reloadingSMGSound;
                    sfx.Play();
                    smgAnimation.SetTrigger("Reloading");
                    yield return new WaitForSeconds(2.5f);
                    smgAmmo = 25;
                    isReloading = false;
                    break;
                case "Shotgun":
                    lastFired = Time.time + 3.5f;
                    sfx.clip = reloadingShotgunSound;
                    sfx.Play();
                    shotgunAnimation.SetTrigger("Reloading");
                    yield return new WaitForSeconds(2.8f);
                    shotgunAmmo = 2;
                    isReloading = false;
                    break;
                default:
                    Debug.LogWarning("I can't detect what weapon you're using or you're unarmed");
                    break;

            }
    }

    void AmmoCounter() // Shows ammo and max ammo
    {
        switch (heldWeapon)
        {
            case "Pistol":
                ammoCount.text = pistolAmmo.ToString() + "/12";
                break;
            case "SMG":
                ammoCount.text = smgAmmo.ToString() + "/25";
                break;
            case "Shotgun":
                ammoCount.text = shotgunAmmo.ToString() + "/2";
                break;
        }
    }

    void Recoil()
    {
        if (heldWeapon != "Shotgun")
        {
            Quaternion randomRecoil = Quaternion.Euler(Random.Range(0.5f, 0.5f), Random.Range(0.5f, -0.1f), 0);
            Transform originalCameraRotation = playerCamera.transform;
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, playerCamera.transform.rotation * randomRecoil, 0.5f);
        }
        else
        {
            Quaternion randomRecoil = Quaternion.Euler(Random.Range(0.5f, 0.5f), Random.Range(1f, -0.1f), 0);
            Transform originalCameraRotation = playerCamera.transform;
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, playerCamera.transform.rotation * randomRecoil, 0.5f);
        }
    }

    IEnumerator WeaponSwapping()
    {
        isSwapping = true;
        // swapping animations
        pistolAnimation.SetTrigger("Swapping");
        smgAnimation.SetTrigger("Swapping");
        shotgunAnimation.SetTrigger("Swapping");
        lastFired = Time.time + 1f;
        
        yield return new WaitForSeconds(0.5f);
        switch (heldWeapon)
        {
            case "Pistol":
                transform.GetChild(0).GetComponent<Renderer>().enabled = true; // Pistol
                transform.GetChild(1).GetComponent<Renderer>().enabled = false; // SMG
                transform.GetChild(2).GetComponent<Renderer>().enabled = false; // Shotgun
                break;
            case "SMG":
                transform.GetChild(0).GetComponent<Renderer>().enabled = false; // Pistol
                transform.GetChild(1).GetComponent<Renderer>().enabled = true; // SMG
                transform.GetChild(2).GetComponent<Renderer>().enabled = false; // Shotgun
                break;
            case "Shotgun":
                transform.GetChild(0).GetComponent<Renderer>().enabled = false; // Pistol
                transform.GetChild(1).GetComponent<Renderer>().enabled = false; // SMG
                transform.GetChild(2).GetComponent<Renderer>().enabled = true; // Shotgun
                break;
        }
        isSwapping = false;
    }
}
