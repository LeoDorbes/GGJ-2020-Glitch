using System;
using System.Collections;
using DG.Tweening;
using FMOD.Studio;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Interactibles
{
    public class PlayerMirror : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Animator _animator;
        private bool _endAnim;
        private bool _hintShown;
        private EventInstance _glitchSound;
        private bool _musicStarted;

        private void Start()
        {
            _glitchSound = Sound.CreateSoundInstance("event:/SD/SOUND_GLITCH_IN");
            _glitchSound.setVolume(1);
            GameManager.I._ambianceManager.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        private void Update()
        {
            var diff = _player.position.x - transform.position.x;
            var volume = 120 / (Mathf.Abs(diff) + 5) * 100 + 1;
            _glitchSound.setVolume(volume);

            if (Mathf.Abs(diff) <= 5f && !_endAnim && !_hintShown)
            {
                _player.GetComponent<PlayerEntity>().ShowActionHint(true);
                _hintShown = true;
            }
            if (Mathf.Abs(diff) <= 5f && Input.GetButtonDown("Jump") && !_endAnim)
            {
                _endAnim = true;

                StartCoroutine(EndAnimation());
            }

            if (transform.position.x < 100 && !_musicStarted)
            {
                _musicStarted = true;
                Sound.PlaySoundOneShot("event:/SD/Amb/Amb_Title");
            }
        }

        private IEnumerator EndAnimation()
        {
            var entity = _player.GetComponent<PlayerEntity>();

            entity.HasControl = false;
            entity._animator.SetTrigger("doAction");
            _animator.SetTrigger("transform");
                
            _player.DOJump(transform.position - new Vector3(1, 0, 0), 5, 1, 2);
            transform.DOJump(_player.position + new Vector3(1, 0, 0), 5, 1, 2);
            yield return new WaitForSeconds(1f);
            Sound.PlaySoundOneShot("event:/SD/SOUND_GET_GLITCHED");
            yield return new WaitForSeconds(1f);

            StartCoroutine(Animations.FadeInCoroutine(1.6f, GameManager.I.GameUi.BlackForeground));
            yield return new WaitForSeconds(2f);

            SceneManager.LoadSceneAsync("OutroDialogue");
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