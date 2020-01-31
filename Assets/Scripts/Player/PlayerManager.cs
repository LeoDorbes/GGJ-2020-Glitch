using UnityEngine;
using Utils;

namespace Scenes.Scripts.Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public PlayerEntity Player { get; set; }
    }
}
