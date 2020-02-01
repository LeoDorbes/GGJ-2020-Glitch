using System;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Ui
{
    public class GameUi : MonoBehaviour
    {
        public Image BlackForeground;

        private void Awake()
        {
            GameManager.I.GameUi = this;
            StartCoroutine(Animations.FadeOutCoroutine(1f, BlackForeground));

        }
    }
}