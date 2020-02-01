using UnityEngine;

namespace Tiles
{
    public class TileEntity : MonoBehaviour
    {
        public bool Glitched { get; private set; }

        private TileGraphicsHandler _graphicsHandler;

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
    }
}