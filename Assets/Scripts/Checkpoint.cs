using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Important")]
    public Transform doorToClose;
    public GameObject antiCamper; // Destroy transparent glasses that stops players from killing before entering
    public Vector3 placeToClose;
    private bool _isClosed = false;

    [Header("Use if Boss Fight only")]
    public AudioSource musicSoundSource;
    public bool isBossFight = false;

    [Header("Enemies")]
    public Enemy[] enemiesToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_isClosed)
        {
            _isClosed = true;
            if (doorToClose != null)
            {
                doorToClose.position = placeToClose;
                Destroy(antiCamper);
                AlertEnemies();
            }
            if (isBossFight) // If it is boss fight, then a special music will play.
            {
                musicSoundSource.Play();
            }
        }
    }

    private void AlertEnemies()
    {
        for (int i = 0; i < enemiesToTrigger.Length; i++)
        {
            enemiesToTrigger[i].hasSpottedPlayer = true;
            // enemiesToTrigger[i].lastFired = 
        }
    }
}
