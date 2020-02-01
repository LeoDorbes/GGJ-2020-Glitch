using System;
using System.Collections;
using FMOD.Studio;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : Singleton<GameManager>
{
    private EventInstance _musicManager;
    private EventInstance _ambianceManager;

    public GameUi GameUi { get; set; }
    void Start()
    {
        _musicManager = Sound.CreateSoundInstance("event:/Music/Music_Generative");
        _ambianceManager = Sound.CreateSoundInstance("event:/SD/Amb/Amb");

        _ambianceManager.start();
        _musicManager.start();
        StartCoroutine(Animations.FadeOutCoroutine(2f, GameUi.BlackForeground));
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCoroutine());
    }

    private IEnumerator LoadNextLevelCoroutine()
    {
        Debug.Log("NEXT LEVELLLLLL");
        var scene = int.Parse(SceneManager.GetActiveScene().name);

        StartCoroutine(Animations.FadeInCoroutine(3f, GameUi.BlackForeground));
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene((scene + 1).ToString());
    }
}
