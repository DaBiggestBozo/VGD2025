using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    public GameObject levelButtons;

    private void Awake() //Disable all buttons but level one
    {
        ButtonsToArray(); //Get Buttons

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }
    public void OpenLevel(int levelID)
    {
        SceneManager.LoadScene(levelID); //load specified scene, main menu is 0
    }

    private void ButtonsToArray() //Calculate the amound of buttons in the gameobject
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
}