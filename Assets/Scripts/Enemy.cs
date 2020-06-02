using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Animator enemyAnimation;
    public Transform player;
    public Transform enemyVision;
    public AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip shotgunFireSound;
    private RaycastHit hit;
    public float visionDistance = 100f;
    public float attackDistance = 100f;
    public float health = 3f;
    public float speed = 12f;
    public float fireRate = 1.2f;
    public bool hasSpottedPlayer = false;
    [HideInInspector]
    public float lastFired;
    private bool walking = false;
    private bool aiming = false;


    [Header("Type of Enemy")]
    public bool isZombie = false;
    public bool usingShotgun = false;

    [Header("Firing Settings")]
    public GameObject bullet;
    public GameObject shell;
    private GameObject firingBullet;
    public float bulletSpeed = 1000f;
    public float offset = 2f;

    [Header("Player Hitmarker")]
    public WeaponSystem weaponSystem;
    public AudioClip hitSound;
    public AudioClip deathHitSound;
    

    void Start()
    {
        RigidbodyState(true);
        ColliderState(false);
    }

    void Update()
    {
        if (health > 0)
        {

            // Only follow if spotted, now scrapped because of lack of time and for performance.

            // if (Physics.Raycast(enemyVision.position, enemyVision.forward, out hit, visionDistance) && !hasSpottedPlayer)
            // {
            //     if (hit.rigidbody != null) // Had issues trying to fix this
            //     {
            //         Debug.DrawRay(enemyVision.position, enemyVision.forward * 20f, Color.red, 1f);
            //         Debug.Log("You have been spotted");
            //         hasSpottedPlayer = true;
            //         lastFired = Time.time + fireRate + 0.7f; // Don't want enemy to shoot instantly upon spotting
            //     }
            // }

            if (hasSpottedPlayer)
            {
                Quaternion originalRotation = transform.rotation;
                transform.LookAt(player);
                enemyVision.LookAt(player);
                Quaternion newRotation = transform.rotation;
                transform.rotation = originalRotation;
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 5f * Time.deltaTime);
                enemyVision.rotation = Quaternion.Slerp(enemyVision.rotation, newRotation, 5f * Time.deltaTime);
            }

            // if player is far away while spotted, enemy shall follow
            if (Vector3.Distance(transform.position, player.position) > attackDistance && hasSpottedPlayer)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                walking = true;
                aiming = false;
            }
            // if enemy is near player, enemy shall aim
            else if (Vector3.Distance(transform.position, player.position) < attackDistance && hasSpottedPlayer && !aiming && !isZombie)
            {
                transform.position = transform.position;
                aiming = true;
            }

            // if enemy is aiming and certain time has passed, shoot while raycasting
            if (!isZombie && aiming && lastFired < Time.time && Physics.Raycast(enemyVision.position, enemyVision.forward, out hit, attackDistance))
            {
                if (hit.rigidbody != null)
                {
                    enemyAnimation.SetTrigger("Shooting");

                    if (usingShotgun)
                    {
                        lastFired = Time.time + fireRate + 2f;
                    }

                    else
                    {
                        lastFired = Time.time + fireRate;
                    }

                    Fire();
                }
            }

            enemyAnimation.SetBool("Walking", walking);
            enemyAnimation.SetBool("Aiming", aiming);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 13)
        {
            health--;
            if (health > 0)
            {
                weaponSystem.sfx.clip = hitSound;
                weaponSystem.sfx.Play();
            }
            else if (health == 0)
            {
                weaponSystem.sfx.clip = deathHitSound;
                weaponSystem.sfx.Play();
                Die();
            }
        }
    }

    void Fire() // Same as in WeaponSystem
    {
        if (!usingShotgun)
        {
            audioSource.clip = fireSound;
            audioSource.Play();

            Vector3 spread = enemyVision.transform.up * Random.Range(-0.1f, 0.1f); // Vertical spread
            spread += enemyVision.transform.right * Random.Range(-0.1f, 0.1f); // Horizontal spread
            Quaternion spreadAngle = Quaternion.AngleAxis(0f, transform.forward + spread);
            firingBullet = Instantiate(bullet, enemyVision.transform.position + enemyVision.transform.forward + spread * offset, enemyVision.transform.rotation);
            Physics.IgnoreCollision(GetComponent<Collider>(), firingBullet.GetComponent<Collider>(), true);
            firingBullet.transform.forward = enemyVision.transform.forward + spread;
            firingBullet.GetComponent<Rigidbody>().velocity = enemyVision.transform.forward * bulletSpeed * Time.deltaTime;
        }
        else
        {
            audioSource.clip = shotgunFireSound;
            audioSource.Play();
            for (int i = 0; i < 8; i++)
            {
                Vector3 spread = enemyVision.transform.up * Random.Range(-0.3f, 0.3f); // Vertical spread
                spread += enemyVision.transform.right * Random.Range(-0.3f, 0.3f); // Horizontal spread
                Quaternion spreadAngle = Quaternion.AngleAxis(0f, enemyVision.transform.forward + spread);
                firingBullet = Instantiate(shell, enemyVision.transform.position + enemyVision.transform.forward + spread * offset, enemyVision.transform.rotation);
                Physics.IgnoreCollision(GetComponent<Collider>(), firingBullet.GetComponent<Collider>(), true);
                firingBullet.transform.forward = enemyVision.transform.forward + spread;
                firingBullet.GetComponent<Rigidbody>().velocity = enemyVision.transform.forward * bulletSpeed * Time.deltaTime;
            }
        }
    }

    void Die()
    {
        // Enemy ragdolls
        enemyAnimation.enabled = false;
        RigidbodyState(false);
        ColliderState(true);
        Debug.Log("enemy dead");
    }

    // Thanks to Firemind for his video and code on death ragdoll
    void RigidbodyState(bool state)
    {

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
    }


    void ColliderState(bool state)
    {

        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }

}
