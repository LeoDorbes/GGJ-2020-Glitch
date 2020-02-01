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
    public GameUi GameUi { get; set; }
    void Start()
    {
        _musicManager = Utils.Sound.CreateSoundInstance("event:/Music/Music_Generative");
        _musicManager.start();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCoroutine());
    }

    private IEnumerator LoadNextLevelCoroutine()
    {
        var scene = int.Parse(SceneManager.GetActiveScene().name);

        StartCoroutine(Animations.FadeOutFadeInCoroutine(3f, GameUi.BlackForeground));
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene((scene + 1).ToString());
        
        
    }
}
