using FMOD.Studio;
using Interactibles;
using Tiles;
using UnityEngine;
using Utils;

namespace Player
{
    public class TileSwitcherHandler : MonoBehaviour
    {
        private CircleCollider2D _collider;
        private EventInstance _entitySound;

        private float rangeMultiplier = 1;

        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
        }

        public void ReduceRange()
        {
            _collider.radius -= 0.1f;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            var tileEntity = other.GetComponent<TileEntity>();

            if (!tileEntity) return;

            tileEntity.glitchRatio = (_collider.radius - Vector2.Distance(transform.position, tileEntity.transform.position)) / _collider.radius;
            tileEntity.SwitchCollided();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var interactibleEntity = other.GetComponent<InteractibleEntity>();

            if (!interactibleEntity) return;

            interactibleEntity.ChangeNeonState(true);
            _entitySound = Sound.CreateSoundInstance("event:/SD/SOUND_GLITCH_IN");
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var tileEntity = other.GetComponent<TileEntity>();
            var interactibleEntity = other.GetComponent<InteractibleEntity>();

            if (tileEntity)
            {
                tileEntity.glitchRatio = 0f;
                tileEntity.SetGlitchedState(false);
            }
            if (interactibleEntity)
            {
                interactibleEntity.ChangeNeonState(false);
                _entitySound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
    }
}