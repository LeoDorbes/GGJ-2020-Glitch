using System;
using System.Collections;
using FMOD.Studio;
using Player;
using Ui;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : Singleton<GameManager>
{
    private EventInstance _musicManager;
    private EventInstance _ambianceManager;

    public GameUi GameUi { get; set; }
    private void Start()
    {
        if (I != this)
        {
            Destroy(gameObject);
        }
        _musicManager = Sound.CreateSoundInstance("event:/Music/Music_Generative");
        _ambianceManager = Sound.CreateSoundInstance("event:/SD/Amb/Amb");

        _ambianceManager.start();
        _musicManager.start();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCoroutine());
    }

    private IEnumerator LoadNextLevelCoroutine()
    {
        PlayerManager.I.Player.HasControl = false;
        var scene = int.Parse(SceneManager.GetActiveScene().name);

        StartCoroutine(Animations.FadeInCoroutine(1f, GameUi.BlackForeground));
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene((scene + 1).ToString());
    }
}
