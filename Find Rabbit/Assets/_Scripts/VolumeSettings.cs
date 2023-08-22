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
}
