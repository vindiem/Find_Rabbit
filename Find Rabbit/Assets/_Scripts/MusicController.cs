using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource musicSource;
    private AudioSource soundSource;

    private AudioSource[] sources;

    private void Start()
    {
        musicSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        soundSource = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>();

        DontDestroyOnLoad(musicSource);
        DontDestroyOnLoad(soundSource);

        sources = GameObject.FindObjectsOfType<AudioSource>();

        for (int i = 0; i < sources.Length; i++)
        {
            if (sources[i] != musicSource && sources[i] != soundSource)
            {
                Destroy(sources[i].gameObject);
            }
        }
    }

    private void Update()
    {
        float musicVolume = PlayerPrefs.GetFloat("Music volume");
        float soundVolume = PlayerPrefs.GetFloat("Sound volume");

        musicSource.volume = musicVolume;
        soundSource.volume = soundVolume;
    }
}
