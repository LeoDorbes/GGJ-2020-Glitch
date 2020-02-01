using System.Collections.Generic;
using Utils;

namespace Tiles
{
    public class TileManager : Singleton<TileManager>
    {
        public List<TileEntity> Tiles { get; set; } = new List<TileEntity>();
    }
}