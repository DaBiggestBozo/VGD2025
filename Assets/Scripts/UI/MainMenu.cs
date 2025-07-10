using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1); //Load scene 1
    }

    public void Exit()
    {
        Application.Quit(); //Quit Game
        Destroy(gameObject); //Destroy camera (so that we know it works, because unity blocks quiting the game in the editor)
    }

    public void Credits()
    {
        SceneManager.LoadSceneAsync(2); //credits are scene 2 //swapping to prefab in both scenes now
    }
}