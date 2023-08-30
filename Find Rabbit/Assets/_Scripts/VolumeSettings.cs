using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;

    public Image musicBar;
    public Image soundBar;

    private float musicSliderValue = 0.5f;
    private float soundSliderValue = 0.5f;

    private void Start()
    {
        musicSliderValue = PlayerPrefs.GetFloat("Music volume");
        soundSliderValue = PlayerPrefs.GetFloat("Sound volume");

        musicSliderValue = (musicSliderValue == 0) ? 1 : musicSliderValue;
        soundSliderValue = (soundSliderValue == 0) ? 1 : soundSliderValue;

        musicSlider.value = musicSliderValue;
        soundSlider.value = soundSliderValue;
    }

    private void Update()
    {
        musicSliderValue = musicSlider.value;
        soundSliderValue = soundSlider.value;

        musicBar.fillAmount = musicSlider.value;
        soundBar.fillAmount = soundSlider.value;

        PlayerPrefs.SetFloat("Music volume", musicSliderValue);
        PlayerPrefs.SetFloat("Sound volume", soundSliderValue);
    }

    public void SetVolumeValue()
    {
        AudioSource musicSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        AudioSource soundSource = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>();

        float musicVolume = PlayerPrefs.GetFloat("Music volume");
        float soundVolume = PlayerPrefs.GetFloat("Sound volume");

        musicSource.volume = musicVolume;
        soundSource.volume = soundVolume;
    }

}
