using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;


public class MainMenu : MonoBehaviour
{

    // For ChangeGraphic function
    public PostProcessVolume postProcess;
    [HideInInspector]
    public AmbientOcclusion ambientOcclusion;
    public Text graphicsText;
    bool fastMode = false;

    void Start()
    {
        postProcess.profile.TryGetSettings(out ambientOcclusion);
    }

    public void PlayGame()
    {
        Debug.Log("You are now playing");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("You now left the game");
        Application.Quit();
    }

    public void ChangeGraphic()
    {
        if (!fastMode)
        {
            ambientOcclusion.enabled.value = false;
            QualitySettings.SetQualityLevel(0);
            graphicsText.text = "Graphics: Fast";
            fastMode = true;
        }
        else
        {
            ambientOcclusion.enabled.value = true;
            QualitySettings.SetQualityLevel(5);
            graphicsText.text = "Graphics: Good";
            fastMode = false;
        }
        
    }
}
