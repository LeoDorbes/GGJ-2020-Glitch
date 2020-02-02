using System;
using System.Collections;
using FMOD.Studio;
using Player;
using Tiles;
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
        
        SetBugLevel();

        _ambianceManager.start();
        _musicManager.start();
    }

    public void SetBugLevel()
    {
        var ratio = Mathf.Clamp(TileManager.I.BugRatio * 100 + 1, 1, 100);
        _musicManager.setParameterByName("bugLevel", ratio);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCoroutine());
    }

    private IEnumerator LoadNextLevelCoroutine()
    {
        PlayerManager.I.Player.HasControl = false;
        var scene = int.Parse(SceneManager.GetActiveScene().name);

        
        if (scene != 2)
        {
            StartCoroutine(Animations.FadeInCoroutine(1f, GameUi.BlackForeground));
            yield return new WaitForSeconds(1.1f);
            SceneManager.LoadScene((scene + 1).ToString());
        }
        else
        {
            _ambianceManager.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            StartCoroutine(Animations.FadeInCoroutine(1.6f, GameUi.BlackForeground));
            _musicManager.setParameterByName("fin", 1);
            yield return new WaitForSeconds(1.8f);
            SceneManager.LoadSceneAsync("LastScene");
        }
        
    }
}
