using System.Collections;
using UnityEngine;

public class ElevatorOpen : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine("Move");
    }

    // Update is called once per frame
    IEnumerator Move()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.position = Vector3.Lerp(transform.position, transform.forward * -10f, 2f * Time.deltaTime);
        }
    }
}
