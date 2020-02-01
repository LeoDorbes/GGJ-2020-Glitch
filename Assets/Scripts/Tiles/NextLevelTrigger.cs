using Player;
using UnityEngine;

namespace Tiles
{
    public class NextLevelTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            
            GameManager.I.LoadNextLevel();
        }
    }
}