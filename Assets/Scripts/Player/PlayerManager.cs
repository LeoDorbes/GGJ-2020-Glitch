using Utils;

namespace Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public PlayerEntity Player { get; set; }
    }
}
