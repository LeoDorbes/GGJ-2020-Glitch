using System;
using UnityEngine;
using Utils;

namespace Tiles
{
    public class TileEntity : MonoBehaviour
    {
        public Point2 Position { get; private set; }
        public bool Glitched { get; private set; }

        private TileGraphicsHandler _graphicsHandler;

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
    }
}