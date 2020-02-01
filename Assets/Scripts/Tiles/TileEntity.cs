using UnityEngine;
using Utils;

namespace Tiles
{
    public class TileEntity : MonoBehaviour
    {
        public Point2 Position { get; private set; }
        public bool Glitched { get; private set; }

        public bool glitchProcessIsOccuring = false;

        public float glitchRatio = 0f;

        public float glitchTimer = 0f;

        public bool isInRandomZone = false;

        public static float randomZone = 0.6f;

        public static float randomChance = 0.1f;

        private TileGraphicsHandler _graphicsHandler;

        public bool isBugged;

        [SerializeField] private BugTilePool bugTilePool;

        private void Awake()
        {
            _graphicsHandler = GetComponent<TileGraphicsHandler>();
        }

        private void Start()
        {
            TileManager.I.Tiles.Add(this);
        }

        public void SetGlitchedState(bool glitched)
        {
            if (Glitched != glitched && glitched)
            {
                if (Random.Range(1, 100) > 10)
                {
                    Sound.PlaySoundOneShot("event:/SD/Footsteps/Footsteps_character", transform);
                }
            }
            
            Glitched = glitched;
            _graphicsHandler.DisplayGlitchedState(glitched);
        }

        public void SwitchCollided()
        {
            _graphicsHandler.StartAmbiantGlitch();
        }
        
        public void SetBugState()
        {
            this.isBugged = true;
            _graphicsHandler.DisplayBug(bugTilePool.bugSprites.RandomElement());
        }
    }
}