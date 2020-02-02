using System;
using DG.Tweening;
using UnityEditor.Animations;
using UnityEngine;

namespace Interactibles
{
    public class PlayerMirror : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Animator _animator;
        private void FixedUpdate()
        {
            var pos = _player.position;
            var posx = 180 - pos.x;
            if (posx < 120)
            {
                
            }
            if (posx < 91)
            {
                posx = pos.x + 2f;
            }

            transform.position = new Vector3(posx, pos.y, pos.z);
        }
    }
}