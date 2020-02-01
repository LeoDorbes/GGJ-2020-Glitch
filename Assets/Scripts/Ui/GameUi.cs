using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class GameUi : MonoBehaviour
    {
        public Image BlackForeground;

        private void Start()
        {
            GameManager.I.GameUi = this;
        }
    }
}