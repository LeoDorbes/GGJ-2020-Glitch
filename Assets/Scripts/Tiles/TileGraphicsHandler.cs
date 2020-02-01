using System.Collections;
using UnityEngine;

namespace Tiles
{
    public class TileGraphicsHandler : MonoBehaviour
    {
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _glitchedSprite;

        private SpriteRenderer _spriteRenderer;
        private TileEntity _tileEntity;

        private void Awake()
        {
            _tileEntity = GetComponent<TileEntity>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void DisplayGlitchedState(bool glitched)
        {
            _spriteRenderer.sprite = glitched ? _glitchedSprite : _normalSprite;
        }
        
        public void StartAmbiantGlitch()
        {
            if (!_tileEntity.glitchProcessIsOccuring)
            {
                _tileEntity.glitchProcessIsOccuring = true;
                StartCoroutine(glitchCoroutine());
            }
        }

        public IEnumerator glitchCoroutine()
        {
            bool randomZoneChanged;
            while (_tileEntity.glitchRatio > 0f)
            {
                randomZoneChanged = false;
                _tileEntity.glitchTimer = 1.5f;
                
                _tileEntity.isInRandomZone = 1 - TileEntity.randomZone > _tileEntity.glitchRatio;
                
                if (_tileEntity.isInRandomZone)
                {
                    _tileEntity.SetGlitchedState(Random.value < TileEntity.randomChance);
                }
                else
                {
                    _tileEntity.SetGlitchedState(true);
                }
                
                while ( !randomZoneChanged &&
                        _tileEntity.glitchTimer > 0)
                {
                    randomZoneChanged = _tileEntity.isInRandomZone != 1-TileEntity.randomZone > _tileEntity.glitchRatio;
                    
                    _tileEntity.glitchTimer -= Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForEndOfFrame();
            }
            
            _tileEntity.glitchProcessIsOccuring = false;
            yield return null;
        }
    }
}