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
            Destroy(gameObject, 5f);
        }
    }

    void OnCollisionEnter(Collision other) // This doesnt work, have to make it despawn upon hitting wall
    {
        if (other.gameObject.tag == "Obstacle" && gameObject != null)
        {
            bulletRigidBody.velocity = Vector3.up;
            bulletRigidBody.useGravity = true;
            gameObject.layer = 0;
        }
    }
}
