using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class SlowTime : MonoBehaviour
{

    public PostProcessVolume postProcess;
    [HideInInspector]
    public ColorGrading colorGrading;
    public AudioSource slowSound;
    public AudioSource music;
    public MouseLook mouseLook;
    public WeaponSystem weaponSystem;
    public Image slowMeter;
    public float Regeneration = 1f;
    public float Consumption = 1f;
    AudioSource[] allSounds;

    private bool isSlowed = false;

    void Start()
    {
        postProcess.profile.TryGetSettings(out colorGrading);
        allSounds = FindObjectsOfType(typeof(AudioSource)) as AudioSource[]; 
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) && !isSlowed && slowMeter.fillAmount > 0.9f)
        {
            isSlowed = true;
            Time.timeScale = 0.5f;
            for (int i = 0; i < allSounds.Length; i++)
            {
                allSounds[i].pitch = 0.7f; // All the sounds will be grabbed and have pitch lowered
            }
            slowSound.Play();
            slowSound.pitch = 1f;
            music.pitch = 1f;
            music.volume = 0.2f;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) && isSlowed || slowMeter.fillAmount <= 0f)
        {
            isSlowed = false;
            slowSound.Stop();
            Time.timeScale = 1f;
            for (int i = 0; i < allSounds.Length; i++)
            {
                allSounds[i].pitch = 1f;
            }
            music.volume = 0.7f;
        }

        // Regeneration
        if (slowMeter.fillAmount < 1 && !isSlowed && Time.timeScale == 1f)
        {
            slowMeter.fillAmount += Regeneration * Time.deltaTime;
            colorGrading.saturation.value = Mathf.Lerp(colorGrading.saturation.value, 20f, 0.3f);
        }

        // Consumption
        else if (slowMeter.fillAmount > 0 && isSlowed && Time.timeScale > 0.1f)
        {
            slowMeter.fillAmount = slowMeter.fillAmount - Consumption * Time.deltaTime;
            colorGrading.saturation.value = Mathf.Lerp(colorGrading.saturation.value, -20f, 0.3f);
        }         
    }
}
