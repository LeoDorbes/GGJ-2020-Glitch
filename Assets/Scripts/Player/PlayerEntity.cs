using System;
using UnityEngine;

namespace Scenes.Scripts.Player
{
    public class PlayerEntity : MonoBehaviour
    {
        private void Start()
        {
            PlayerManager.I.Player = this;
        }
    }
}