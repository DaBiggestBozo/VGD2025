using UnityEngine;
using UnityEngine.UI;
//using static UnityEditorInternal.ReorderableList;

public class SensitivtySettings : MonoBehaviour
{
    [SerializeField] private Slider sensitivityXSlider;
    [SerializeField] private Slider sensitivityYSlider;
    private static float DefaultSensX = 1000f;
    private static float DefaultSensY = 1000f;



    private void Start()
    {
        LoadSensitivity();
        SetSensitivityX();
        SetSensitivityY();
    }

    public void SetSensitivityX()
    {
        float sensitiviyX = sensitivityXSlider.value;
        PlayerPrefs.SetFloat("sensitiviyX", sensitiviyX);
    }
    public void SetSensitivityY()
    {
        float sensitiviyY = sensitivityYSlider.value;
        PlayerPrefs.SetFloat("sensitiviyY", sensitiviyY);
    }

    public void LoadSensitivity()
    {
        if (PlayerPrefs.HasKey("sensitiviyX"))
        {
            sensitivityXSlider.value = PlayerPrefs.GetFloat("sensitiviyX");
        }

        if (PlayerPrefs.HasKey("sensitiviyY"))
        {
            sensitivityYSlider.value = PlayerPrefs.GetFloat("sensitiviyY");
        }
    }

    public void SavePrefs()
    {
        PlayerPrefs.Save();
    }
    public void ResetSensX()
    {
        PlayerPrefs.SetFloat("sensitiviyX", DefaultSensX);
        sensitivityXSlider.value = DefaultSensX;
    }

    public void ResetSensY()
    {
        PlayerPrefs.SetFloat("sensitiviyY", DefaultSensY);
        sensitivityYSlider.value = DefaultSensY;
    }

}
