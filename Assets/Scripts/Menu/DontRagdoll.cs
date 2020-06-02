using UnityEngine;

public class DontRagdoll : MonoBehaviour
{
    void Start()
    {
        RigidbodyState(true);
        ColliderState(false);
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
