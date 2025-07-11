//using Palmmedia.ReportGenerator.Core;
using UnityEngine;

public class CheckPause : MonoBehaviour
{
    [SerializeField] private Pause pause;
    [SerializeField] private SensitivtySettings sensSettings;
    [SerializeField] private VolumeSettings volumeSettings;
    [SerializeField] private PlayerCam playerCam;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Escape)) //Alpha 1 (one key above keyboard) exists for testing, as unity does not register escape inputs in editor mode
        {
            checkMenuAction();
        }
    }

    void checkMenuAction() //check whether to open or close a menu, and which one
    {
        if (settingsPanel.activeInHierarchy)
        {
            sensSettings.SavePrefs();
            volumeSettings.SavePrefs();
            pauseMenu.SetActive(true);
            settingsPanel.SetActive(false);
            playerCam.GetSens();
        } else if (pauseMenu.activeInHierarchy)
        {
            pause.Resume();
        } else
        {
            pause.StartPause();
        }
    }
}
