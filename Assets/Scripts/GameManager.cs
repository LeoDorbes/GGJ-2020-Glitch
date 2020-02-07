using System;
using System.Collections;
using DG.Tweening;
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
    public EventInstance _ambianceManager;
    private bool _nextSceneTriggered;

    private bool _musicStarted;

    public GameUi GameUi { get; set; }
    private void Start()
    {

        if (!_musicStarted)
        {
            _musicManager = Sound.CreateSoundInstance("event:/Music/Music_Generative");
            _ambianceManager = Sound.CreateSoundInstance("event:/SD/Amb/Amb");
            _ambianceManager.start();
            _musicManager.start();
            _musicStarted = true;
        }

        SceneManager.sceneLoaded += (arg0, mode) => _nextSceneTriggered = false;
    }

    private void OnSceneLoaded()
    {
        
    }

    public void SetBugLevel()
    {
        var ratio = Mathf.Clamp(TileManager.I.BugRatio * 15 * 100 + 1, 1, 100);
        _musicManager.setParameterByName("bugLevel", ratio);
    }

    public void LoadNextLevel()
    {
        if (!_nextSceneTriggered)
        {
            _nextSceneTriggered = true;
            StartCoroutine(LoadNextLevelCoroutine());
        }
    }

    private IEnumerator LoadNextLevelCoroutine()
    {
        PlayerManager.I.Player.HasControl = false;
        Sound.PlaySoundOneShot("event:/SD/SOUND_WARP_OUT", PlayerManager.I.Player.transform);
        PlayerManager.I.Player.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 1.5f);
        PlayerManager.I.Player.transform.DORotate(new Vector3(0, 0, 180), 1.5f);
        yield return new WaitForSeconds(2f);
        var scene = int.Parse(SceneManager.GetActiveScene().name);

        
        if (scene < 4)
        {
            StartCoroutine(Animations.FadeInCoroutine(1f, GameUi.BlackForeground));
            yield return new WaitForSeconds(2f);
            SceneManager.LoadSceneAsync((scene + 1).ToString());
        }
        else
        {
            _ambianceManager.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            StartCoroutine(Animations.FadeInCoroutine(1f, GameUi.BlackForeground));
            _musicManager.setParameterByName("fin", 1);
            yield return new WaitForSeconds(1.4f);
            TileManager.I.BugRatio = 0;
            
            SceneManager.LoadSceneAsync("LastScene");
            
        }
    }
}
