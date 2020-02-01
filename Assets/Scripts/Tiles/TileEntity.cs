using UnityEngine;

namespace Tiles
{
    public class TileEntity : MonoBehaviour
    {
        public bool Glitched { get; private set; }

        public bool glitchProcessIsOccuring = false;

        public float glitchRatio = 0f;

        public float glitchTimer = 0f;
        
        public bool isInRandomZone = false;
        
        public static float randomZone = 0.6f;
        
        public static float randomChance = 0.1f;
        
        private TileGraphicsHandler _graphicsHandler;

        public void UpdateRatio()
        {
            
        }
        
        private void Awake()
        {
            TileManager.I.Tiles.Add(this);
            _graphicsHandler = GetComponent<TileGraphicsHandler>();
        }

        public void SetGlitchedState(bool glitched)
        {
            Glitched = glitched;
            _graphicsHandler.DisplayGlitchedState(glitched);
        }

        public void SwitchCollided()
        {
            _graphicsHandler.StartAmbiantGlitch();
        }
        

    }
}