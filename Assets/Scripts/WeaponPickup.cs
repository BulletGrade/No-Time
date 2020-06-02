using UnityEngine;
using UnityEngine.UI;

public class WeaponPickup : MonoBehaviour
{

    public WeaponSystem weaponSystem;
    public Text text;
    public AudioSource pickUpSound;

    void OnTriggerEnter(Collider other)
   {
       if (other.gameObject.tag == "Player")
        {
            switch (gameObject.name)
            {
                case "PistolPickUp":
                    weaponSystem.hasPistol = true;
                    text.text = "Pistol picked up";
                    pickUpSound.Play();
                    pickUpSound.pitch = 0.9f;
                    Destroy(gameObject);
                    break;
                case "SMGPickUp":
                    weaponSystem.hasSMG = true;
                    text.text = "SMG picked up";
                    pickUpSound.Play();
                    pickUpSound.pitch = 0.8f;
                    Destroy(gameObject);
                    break;
                case "ShotgunPickUp":
                    weaponSystem.hasShotgun = true;
                    text.text = "Shotgun picked up";
                    pickUpSound.Play();
                    pickUpSound.pitch = 0.6f;
                    Destroy(gameObject);
                    break;

                default:
                    Debug.LogError("This is not a known weapon. Please Rename!");
                    break;
            }
        }
   }
}
