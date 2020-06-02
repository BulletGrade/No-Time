using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform doorToClose;
    public Vector3 placeToClose;
    bool isClosed = false;
    [Header("Use if Boss Fight only")]
    public AudioSource playerSoundSource;
    public bool isBossFight = false;
    // Warning! What you will see below is completely gross.
    // Unfortunately, I didn't have the time to refactor this.
    [Header("Enemies")]
    public Enemy enemy01 = null;
    public Enemy enemy02 = null;
    public Enemy enemy03 = null;
    public Enemy enemy04 = null;
    public Enemy enemy05 = null;
    public Enemy enemy06 = null;
    public Enemy enemy07 = null;
    public Enemy enemy08 = null;
    public Enemy enemy09 = null;
    public Enemy enemy10 = null;
    public Enemy enemy11 = null;
    public Enemy enemy12 = null;
    public Enemy enemy13 = null;
    public Enemy enemy14 = null;
    public Enemy enemy15 = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isClosed)
        {
            isClosed = true;
            if (doorToClose != null)
            {
                doorToClose.position = placeToClose;
            }
            if (isBossFight) // If it is boss fight, then a special music will play.
            {
                playerSoundSource.Play();
            }
            // not safe for your eyes.
            if (enemy01 != null)
            {
                enemy01.hasSpottedPlayer = true;
                enemy01.lastFired = Time.time + enemy01.fireRate + 1.5f;
            }
            if (enemy02 != null)
            {
                enemy02.hasSpottedPlayer = true;
                enemy02.lastFired = Time.time + enemy02.fireRate + 1.5f;
            }
            if (enemy03 != null)
            {
                enemy03.hasSpottedPlayer = true;
                enemy03.lastFired = Time.time + enemy03.fireRate + 1.5f;
            }
            if (enemy04 != null)
            {
                enemy04.hasSpottedPlayer = true;
                enemy04.lastFired = Time.time + enemy04.fireRate + 1.5f;
            }
            if (enemy05 != null)
            {
                enemy05.hasSpottedPlayer = true;
                enemy05.lastFired = Time.time + enemy05.fireRate + 1.5f;
            }
            if (enemy06 != null)
            {
                enemy06.hasSpottedPlayer = true;
                enemy06.lastFired = Time.time + enemy06.fireRate + 1.5f;
            }
            if (enemy07 != null)
            {
                enemy07.hasSpottedPlayer = true;
                enemy07.lastFired = Time.time + enemy07.fireRate + 1.5f;
            }
            if (enemy08 != null)
            {
                enemy08.hasSpottedPlayer = true;
                enemy08.lastFired = Time.time + enemy08.fireRate + 1.5f;
            }
            if (enemy09 != null)
            {
                enemy09.hasSpottedPlayer = true;
                enemy09.lastFired = Time.time + enemy09.fireRate + 1.5f;
            }
            if (enemy10 != null)
            {
                enemy10.hasSpottedPlayer = true;
                enemy10.lastFired = Time.time + enemy10.fireRate + 1.5f;
            }
            if (enemy11 != null)
            {
                enemy11.hasSpottedPlayer = true;
                enemy11.lastFired = Time.time + enemy11.fireRate + 1.5f;
            }
            if (enemy12 != null)
            {
                enemy12.hasSpottedPlayer = true;
                enemy12.lastFired = Time.time + enemy12.fireRate + 1.5f;
            }
            if (enemy13 != null)
            {
                enemy13.hasSpottedPlayer = true;
                enemy13.lastFired = Time.time + enemy13.fireRate + 1.5f;
            }
            if (enemy14 != null)
            {
                enemy14.hasSpottedPlayer = true;
                enemy14.lastFired = Time.time + enemy14.fireRate + 1.5f;
            }
            if (enemy15 != null)
            {
                enemy15.hasSpottedPlayer = true;
                enemy15.lastFired = Time.time + enemy15.fireRate + 1.5f;
            }
        }
    }
}
