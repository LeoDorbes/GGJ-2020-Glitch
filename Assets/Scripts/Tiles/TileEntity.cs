using System;
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

        public void UpdateRatio()
        {
            
        }
        
        private void Awake()
        {
            _graphicsHandler = GetComponent<TileGraphicsHandler>();
        }

        private void Start()
        {
            Position = new Point2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));

            TileManager.I.RegisterTile(Position, this);
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