using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactibles
{
    public class InteractibleGraphicsHandler : MonoBehaviour
    {
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _neonSprite;
        [SerializeField] private List<Sprite> _glitchedSprite;

        public InteractibleEntity Entity { get; set; }

        private SpriteRenderer _spriteRenderer;
        private bool _isNeon;
        private Coroutine _glitchAnimationCoroutine;

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
            if (_glitchAnimationCoroutine != null) StopCoroutine(_glitchAnimationCoroutine);
            if (!isNeon)
            {
                _spriteRenderer.sprite = Entity.Glitched ? null : _normalSprite;
            }
            else
            {
                if (Entity.Glitched)
                {
                    _glitchAnimationCoroutine = StartCoroutine(AnimateGlitchedState());
                }
                else
                {
                    _spriteRenderer.sprite = _neonSprite;
                }
            }
        }

        public void GlitchStateUpdated()
        {
            NeonStateUpdated(_isNeon);
        }

        private IEnumerator AnimateGlitchedState()
        {
            var i = 0;
            
            while (true)
            {
                _spriteRenderer.sprite = _glitchedSprite[i++];
                i %= _glitchedSprite.Count;
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}