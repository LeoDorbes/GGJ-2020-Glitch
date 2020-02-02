using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


namespace Menu
{
    public class MenuHandler : MonoBehaviour
    {
        [SerializeField] public Image blackImage;
        
        [SerializeField] public Image buttonImage;
        
        [SerializeField] public List<Image> images;

        public static float frameDuration = 0.25f;
        
        public void StartGame()
        {
            StartCoroutine(launchGameCoroutine());
        }

        public IEnumerator launchGameCoroutine()
        {
            int i = 0;
            foreach (var image in images)
            {
                i++;
                if (i == images.Count)
                {
                    frameDuration = 3;
                    buttonImage.sprite = image.sprite;
                    yield return new WaitForSeconds(1.5f);
                    blackImage.DOColor(new Color(0, 0, 0, 1), 3f);
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