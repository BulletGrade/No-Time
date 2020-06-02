using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PlayerDashing : MonoBehaviour
{
    [Header("Dashing Settings")]
    public PostProcessVolume postProcess;
    [HideInInspector]
    public ChromaticAberration chromaticAberration;
    public AudioSource dashWoosh;
    public AudioSource dashReadyBeep;
    public Rigidbody playerRigidbody;
    public Camera playerCamera;
    public Text text;
    public float maxDistance = 6f;
    public float dashCooldown = 3f;
    private float lastTimeUsedDash;

    void Start()
    {
        postProcess.profile.TryGetSettings(out chromaticAberration);
    }

    void Update()
    {
        if (Input.GetButtonDown("Dash"))
        {
            StartCoroutine("CheckIfAbleToDash");
        }
    }

    IEnumerator CheckIfAbleToDash()
    {
        if (Time.time > lastTimeUsedDash)
        {
            if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, maxDistance))
            {
                lastTimeUsedDash = Time.time + dashCooldown;
                StartCoroutine("ReadyBeep");
                for (int i = 0; i < 7; i++)
                {
                    yield return new WaitForSeconds(0.001f);
                    playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 50, 1f * Time.deltaTime);
                    chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, 1f, 3f * Time.deltaTime);
                }
                dashWoosh.Play();
                Debug.DrawRay(transform.parent.position, transform.parent.forward * maxDistance, Color.red, 0.5f);
                chromaticAberration.intensity.value = 0f;
                playerCamera.fieldOfView = 90;
                transform.parent.position = transform.parent.position + playerCamera.transform.forward * maxDistance / 1.2f;
                playerRigidbody.velocity = transform.forward * 2f;
                yield break;

            }
            else
            {
                text.text = ("Can't dash there");
                yield return new WaitForSeconds(2.5f);
                text.text = ("");
            }
        }
        else
        {
            text.text = ("Can't dash yet");
            yield return new WaitForSeconds(2.5f);
            text.text = ("");
        }
    }

    IEnumerator ReadyBeep()
    {
        yield return new WaitForSeconds(dashCooldown);
        dashReadyBeep.Play();
    }
}
