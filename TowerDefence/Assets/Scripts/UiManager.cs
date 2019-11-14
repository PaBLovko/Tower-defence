using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour {

    [SerializeField]
    AudioSource NewGameMusic;


    [SerializeField]
    AudioSource LevelMusic;

    public AudioMixerGroup Mixer;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewGame() {
        LevelMusic.Stop();
        NewGameMusic.Play();
        SceneManager.LoadScene(1);
    }

    public void Settings() {
        SceneManager.LoadScene(2);
    }

    public void Exit() {
        Application.Quit();
    }

    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeEffectVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("EffectVolume", Mathf.Lerp(-40, -20, volume));
    }

    public void ChangeMusicVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-40, -20, volume));
    }

}
