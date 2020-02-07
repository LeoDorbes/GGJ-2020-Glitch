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

            if (!_isBoxCollider2D)
            {
                _playerCollider.enabled = !_playerCollider.enabled;
            }
            else
            {
                var targetSize = Vector2.zero;
                if (Glitched)
                {
                    targetSize = _colliderInvertSize;
                }
                ((BoxCollider2D)_playerCollider).size = _colliderInvertSize;
                DOTween.To(()=> ((BoxCollider2D)_playerCollider).size, x=> ((BoxCollider2D)_playerCollider).size = x, targetSize, .5f);
            }
            
            _graphicsHandler.GlitchStateUpdated();
        }

        public void ChangeNeonState(bool isNeon)
        {
            _graphicsHandler.NeonStateUpdated(isNeon);
        }
    }
}