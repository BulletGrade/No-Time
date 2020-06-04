using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }
}
