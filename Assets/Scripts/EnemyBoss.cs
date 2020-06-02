using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBoss : MonoBehaviour
{
    // The only thing this script will do is check health every frame
    // If boss dies, then player wins.
    Enemy bossEnemy;

    void Start()
    {
        bossEnemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossEnemy.health <= 0)
        {
            StartCoroutine("Die");
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
