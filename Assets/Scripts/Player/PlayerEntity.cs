using UnityEngine;

namespace Player
{
    public class PlayerEntity : MonoBehaviour
    {
        private void Awake()
        {
            PlayerManager.I.Player = this;
        }
    }
}