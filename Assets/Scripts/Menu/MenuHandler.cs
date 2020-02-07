using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Utils;

namespace Menu
{
    public class MenuHandler : MonoBehaviour
    {
        [SerializeField] public Image blackImage;
        
        [SerializeField] public Image buttonImage;
        
        [SerializeField] public List<Image> images;

        public static float frameDuration = 0.25f;

        private bool _gameStarted;
        
        public void StartGame()
        {
            if (!_gameStarted)
            {
                StartCoroutine(launchGameCoroutine());
                _gameStarted = true;
            }
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump") && !_gameStarted)
            {
                _gameStarted = true;
                StartCoroutine(launchGameCoroutine());
            }
        }

        public IEnumerator launchGameCoroutine()
        {
            Sound.PlaySoundOneShot("event:/SD/SOUND_GET_GLITCHED");
            int i = 0;
            foreach (var image in images)
            {
                i++;
                if (i == images.Count)
                {
                    frameDuration = 3;
                    buttonImage.sprite = image.sprite;
                    yield return new WaitForSeconds(1.5f);
                    blackImage.DOColor(new Color(0, 0, 0, 1), 1f);
                }
                buttonImage.sprite = image.sprite;
                yield return new WaitForSeconds(frameDuration);
            }
            
            SceneManager.LoadScene("IntroDialogue");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}