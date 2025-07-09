using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0); //0 is scene id for main menu
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetString("Respawning", "true");
        PlayerPrefs.Save();
        print(PlayerPrefs.GetString("Respawning"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Re-load the current scene
    }

    public void StartPause()
    {
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

}
