
using System.Collections.Generic;
using UnityEngine;

namespace Tiles
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BugTilePool", order = 1)]
    public class BugTilePool : ScriptableObject
    {
        public List<Sprite> bugSprites;
        
    }
}