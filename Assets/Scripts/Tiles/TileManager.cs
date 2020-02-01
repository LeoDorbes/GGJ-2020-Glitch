using System.Collections.Generic;
using Utils;

namespace Tiles
{
    public class TileManager : Singleton<TileManager>
    {
        public Dictionary<Point2, List<TileEntity>> Tiles { get; set; } = new Dictionary<Point2, List<TileEntity>>();

        public void RegisterTile(Point2 position, TileEntity tile)
        {
            if (!Tiles.ContainsKey(position))
            {
                Tiles[position] = new List<TileEntity>();
            }
            Tiles[position].Add(tile);
        }

        public void InvertGlitchedStateAtPosition(Point2 position)
        {
            if (Tiles.TryGetValue(position, out var tiles))
            {
                tiles.ForEach(tile => tile.SetGlitchedState(!tile.Glitched));
            }
        }
    }
}