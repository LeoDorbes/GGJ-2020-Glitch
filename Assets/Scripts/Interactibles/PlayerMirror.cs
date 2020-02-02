using System;
using DG.Tweening;
using FMOD.Studio;
using Player;
using UnityEditor.Animations;
using UnityEngine;
using Utils;

namespace Interactibles
{
    public class PlayerMirror : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Animator _animator;
        private bool _endAnim;
        private EventInstance _glitchSound;

        private void Start()
        {
            _glitchSound = Sound.CreateSoundInstance("event:/SD/SOUND_GLITCH_IN");
            _glitchSound.set3DAttributes()
            _glitchSound.setVolume(1);
        }
        private void Update()
        {
            var diff = _player.position.x - transform.position.x;
            var volume = 120 / (Mathf.Abs(diff) + 5) * 100 + 1;
            _glitchSound.setVolume(volume);
            if (diff <= 3f && Input.GetButtonDown("Jump"))
            {
                _endAnim = true;
                _player.GetComponent<PlayerEntity>().HasControl = false;
                _player.DOScale(new Vector3(3, 3, 3), 6f);
                transform.DOScale(new Vector3(3, 3, 3), 6f);
                
                _player.DOJump(transform.position - new Vector3(1, 0, 0), 5, 1, 10f);
                transform.DOJump(_player.position + new Vector3(1, 0, 0), 5, 1, 10f);

            }
        }

        private void FixedUpdate()
        {
            if (_endAnim)
            {
                return;
            }
            
            var pos = _player.position;
            var posx = 180 - pos.x;
            if (posx < 91)
            {
                posx = pos.x + 2f;
            }

            transform.position = new Vector3(posx, pos.y, pos.z);
        }
    }
}