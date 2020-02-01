using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Tiles
{
    public class TileManager : Singleton<TileManager>
    {
        public float bugRatio;
        public List<TileEntity> Tiles { get; set; } = new List<TileEntity>();

        public void addBugs(float bugRatio)
        {
            foreach (var tile in Tiles)
            {
                if (Random.value < bugRatio)
                {
                    tile.SetBugState();
                }
            }
        }
    }
}