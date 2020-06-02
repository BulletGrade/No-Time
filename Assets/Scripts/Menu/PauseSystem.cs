using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource music;
    bool isPaused = false;
    void Update()
    {
        // Fix later
        if (Input.GetButtonDown("Pause") )
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                music.volume = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pauseMenu.gameObject.SetActive(true);
                isPaused = true;
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
            Time.timeScale = 1f;
            music.volume = 0.7f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenu.gameObject.SetActive(false);
            isPaused = false;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
