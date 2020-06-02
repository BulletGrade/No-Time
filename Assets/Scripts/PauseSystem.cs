using UnityEngine;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour
{
    public Text guide;
    bool isPaused = false;
    void Update()
    {
        // Fix later
        if (Input.GetButtonDown("Pause") && !isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
            guide.text = "Paused";
        }
        else if (Input.GetButtonDown("Pause") && isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
            guide.text = "";
        }
    }
}
