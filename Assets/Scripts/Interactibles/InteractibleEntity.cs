using UnityEngine;
using Utils;

namespace Interactibles
{
    public class InteractibleEntity : MonoBehaviour
    {
        [SerializeField] private Collider2D _playerCollider;
        [SerializeField] private bool _glitchable;
        [SerializeField] private bool _glitched = true;

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
            
            _playerCollider.enabled = !_playerCollider.enabled;
            _graphicsHandler.GlitchStateUpdated();
        }

        public void ChangeNeonState(bool isNeon)
        {
            _graphicsHandler.NeonStateUpdated(isNeon);
        }
    }
}