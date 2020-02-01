using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tiles
{
    public class TileGraphicsHandler : MonoBehaviour
    {
        [SerializeField] private Sprite _normalSprite;
        [FormerlySerializedAs("_glitchedSprite")] [SerializeField] private Sprite _neonSprite;

        private SpriteRenderer _spriteRenderer;
        private TileEntity _tileEntity;

        private void Awake()
        {
            _tileEntity = GetComponent<TileEntity>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void DisplayGlitchedState(bool glitched)
        {
            if(!_tileEntity.isBugged)
                _spriteRenderer.sprite = glitched ? _neonSprite : _normalSprite;
        }

        public void DisplayBug(Sprite buggedSprite)
        {
            _spriteRenderer.sprite = buggedSprite;
        }
        
        public void StartAmbiantGlitch()
        {
            if (!_tileEntity.glitchProcessIsOccuring && !_tileEntity.isBugged)
            {
                _tileEntity.glitchProcessIsOccuring = true;
                StartCoroutine(glitchCoroutine());
            }
        }

        public IEnumerator glitchCoroutine()
        {
            bool randomZoneChanged;
            while (_tileEntity.glitchRatio > 0f && !_tileEntity.isBugged)
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