using UnityEngine;

namespace Interactibles
{
    public class InteractibleGraphicsHandler : MonoBehaviour
    {
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _neonSprite;
        [SerializeField] private Sprite _glitchedSprite;

        public InteractibleEntity Entity { get; set; }

        private SpriteRenderer _spriteRenderer;
        private bool _isNeon;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if (Entity.Glitched && !_isNeon)
            {
                _spriteRenderer.sprite = null;
            }
        }

        public void NeonStateUpdated(bool isNeon)
        {
            _isNeon = isNeon;
            if (!isNeon)
            {
                _spriteRenderer.sprite = Entity.Glitched ? null : _normalSprite;
            }
            else
            {
                _spriteRenderer.sprite = Entity.Glitched ? _glitchedSprite : _neonSprite;
            }
        }

        public void GlitchStateUpdated()
        {
            NeonStateUpdated(_isNeon);
        }
    }
}