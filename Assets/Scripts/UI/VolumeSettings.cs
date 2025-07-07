using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    //[SerializeField] private AudioMixer myMixer; //Will need to assign in inspector
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    private static float DefaultMusic = 0.5f;
    private static float DefaultSFX = 0.5f;

    private void Start()
    {
        LoadVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMusicVolume()
    {
        float musicVolume = musicSlider.value;
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        //myMixer.SetFloat("music", Mathf.Log10(musicVolume) *20); //will need to exposed in audio mixer
    }
    public void SetSFXVolume()
    {
        float SFXVolume = SFXSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
        //myMixer.SetFloat("SFX", Mathf.Log10(SFXVolume) * 20); //will need to exposed in audio mixer
    }

    public void LoadVolume()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    public void SavePrefs()
    {
        PlayerPrefs.Save();
    }

    public void ResetMusic()
    {
        PlayerPrefs.SetFloat("musicVolume", DefaultMusic);
        musicSlider.value = DefaultMusic;
    }
    public void ResetSFX()
    {
        PlayerPrefs.SetFloat("SFXVolume", DefaultSFX);
        SFXSlider.value = DefaultSFX;
    }

}
