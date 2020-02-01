using UnityEngine;

namespace Tiles
{
    public class TileGraphicsHandler : MonoBehaviour
    {
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _glitchedSprite;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void DisplayGlitchedState(bool glitched)
        {
            _spriteRenderer.sprite = glitched ? _glitchedSprite : _normalSprite;
        }
    }
}