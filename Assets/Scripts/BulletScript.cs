using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Rigidbody bulletRigidBody;
    public float despawnTime = 5f;


    void OnEnable()
    {
        if (gameObject != null)
        {
            Destroy(gameObject, despawnTime);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9 && gameObject != null)
        {
            bulletRigidBody.velocity = Vector3.up;
            bulletRigidBody.useGravity = true;
            gameObject.layer = 0;
        }
        else if (other.gameObject.layer == 17 && gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
