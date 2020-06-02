using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Text guide;
    public Camera playerCamera;
    public PlayerMovement playerMovement;
    public Image deadScreen;
    bool isDead = false;


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 13 || other.gameObject.layer == 16) // If shot by bullets, player dies.
        {
            guide.text = ("You're dead");
            StartCoroutine("Death");
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == 15)
        {
            StartCoroutine("Death");
        }
    }

    IEnumerator Death()
    {
        if (!isDead)
        {
            playerMovement.speed = 0f;
            playerMovement.jumpHeight = 0f;
            isDead = true;
            for (int i = 0; i < 20; i++)
            {
                yield return new WaitForSeconds(0.01f);
                deadScreen.color = Color.Lerp(deadScreen.color, Color.red, 0.4f);
            }
            yield return new WaitForSeconds(3f);
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
