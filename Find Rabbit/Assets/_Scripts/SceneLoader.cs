using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AudioSource soundSource;
    [SerializeField] private AudioClip clickSound;

    private void Start()
    {
        soundSource = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>();
    }

    public void LoadScenes(string sceneName)
    {
        soundSource.PlayOneShot(clickSound);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName);
    }
}
