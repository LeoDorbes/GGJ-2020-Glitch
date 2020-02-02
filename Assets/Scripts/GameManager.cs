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
        Debug.Log(TileManager.I.BugRatio);
        Debug.Log(ratio);
        _musicManager.setParameterByName("bugLevel", ratio);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCoroutine());
    }

    private IEnumerator LoadNextLevelCoroutine()
    {
        PlayerManager.I.Player.HasControl = false;
        PlayerManager.I.Player.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 1.5f);
        PlayerManager.I.Player.transform.DORotate(new Vector3(0, 0, 180), 1.5f);
        yield return new WaitForSeconds(2f);
        var scene = int.Parse(SceneManager.GetActiveScene().name);

        
        if (scene < 2)
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
            TileManager.I.BugRatio = 0;
            SetBugLevel();
            
            SceneManager.LoadSceneAsync("LastScene");
            
        }
    }
}
