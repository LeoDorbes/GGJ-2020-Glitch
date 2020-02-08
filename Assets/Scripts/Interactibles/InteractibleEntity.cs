using System;
using DG.Tweening;
using UnityEditor.Tilemaps;
using UnityEngine;
using Utils;

namespace Interactibles
{
    public class InteractibleEntity : MonoBehaviour
    {
        [SerializeField] private Collider2D _playerCollider;
        [SerializeField] private bool _glitchable;
        [SerializeField] private bool _glitched = true;
        [SerializeField] private bool _isBoxCollider2D;

        private Vector2 _colliderInvertSize;

        public bool Glitched
        {
            get => _glitched;
            private set => _glitched = value;
        }
        public bool AlreadyInteracted { get; set; } = false;

        private InteractibleGraphicsHandler _graphicsHandler { get; set; }

        private void Awake()
        {
            _graphicsHandler = GetComponent<InteractibleGraphicsHandler>();
            _graphicsHandler.Entity = this;
        }

        private void Start()
        {
            var boxCollider2D = _playerCollider as BoxCollider2D;
            if (boxCollider2D != null)
            {
                _colliderInvertSize = boxCollider2D.size;
            }
        }

        public void ChangeGlitchState()
        {
            if (!_glitchable) return;

            Glitched = !Glitched;

            var soundPath = "event:/SD/SOUND_GET_UNGLITCHED";
            
            if (Glitched)
            {
                soundPath = "event:/SD/SOUND_GET_GLITCHED";
            }
            
            Sound.PlaySoundOneShot(soundPath, transform);

            if (!Glitched || !_isBoxCollider2D)
            {
                _playerCollider.enabled = !_playerCollider.enabled;
            }
            else
            {
                var playerCollider = (BoxCollider2D)_playerCollider;
                playerCollider.size = new Vector2(0, _colliderInvertSize.y);
                _playerCollider.enabled = true;
                DOTween.To(()=> playerCollider.size, x=> playerCollider.size = x, _colliderInvertSize, .5f);
            }
            
            _graphicsHandler.GlitchStateUpdated();
        }

        public void ChangeNeonState(bool isNeon)
        {
            _graphicsHandler.NeonStateUpdated(isNeon);
        }
    }
}