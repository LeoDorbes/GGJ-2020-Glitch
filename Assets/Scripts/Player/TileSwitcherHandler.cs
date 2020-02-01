using Tiles;
using UnityEngine;

namespace Player
{
    public class TileSwitcherHandler : MonoBehaviour
    {
        private CircleCollider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
        }

        public void ReduceRange()
        {
            _collider.radius -= 0.1f;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var tileEntity = other.GetComponent<TileEntity>();

            if (!tileEntity) return;
            tileEntity.SetGlitchedState(true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var tileEntity = other.GetComponent<TileEntity>();

            if (!tileEntity) return;
            tileEntity.SetGlitchedState(false);
        }
    }
}