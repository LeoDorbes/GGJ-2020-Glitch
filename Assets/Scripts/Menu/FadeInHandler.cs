using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


namespace Menu
{
    public class FadeInHandler : MonoBehaviour
    {
        [SerializeField] public Image blackImage;
        
        public void Awake()
        {
            blackImage.DOColor(new Color(0, 0, 0, 0), 3f);
        }
    }
}