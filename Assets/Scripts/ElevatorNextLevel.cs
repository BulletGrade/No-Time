using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorNextLevel : MonoBehaviour
{
    public Transform elevatorDoor;
    public Vector3 placeToClose;
    bool isClosed = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isClosed)
        {
            isClosed = true;
            StartCoroutine("Move");
        }
    }

    IEnumerator Move()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.position = Vector3.Lerp(transform.position, placeToClose, 1f * Time.deltaTime);
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
