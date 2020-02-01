using System;
using Tiles;
using UnityEngine;

namespace Player
{
    public class TileSwitcherHandler : MonoBehaviour
    {
        private CircleCollider2D _collider;

        private float rangeMultiplier = 1;
        
        
        
        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
        }

        public void ReduceRange()
        {
            _collider.radius -= 0.1f;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            var tileEntity = other.GetComponent<TileEntity>();
            
            if (!tileEntity) return;
            
            tileEntity.glitchRatio = (_collider.radius - Vector2.Distance(transform.position, tileEntity.transform.position)) / _collider.radius;

            tileEntity.SwitchCollided();
        }
        
        /*
        private void OnTriggerEnter2D(Collider2D other)
        {
            var tileEntity = other.GetComponent<TileEntity>();
    
            if (!tileEntity) return;

            Vector2.Distance(transform.position, tileEntity.transform.position);
            tileEntity.SetGlitchedState(true);
        }
        */
        
        private void OnTriggerExit2D(Collider2D other)
        {
            var tileEntity = other.GetComponent<TileEntity>();

            if (!tileEntity) return;
            
            tileEntity.glitchRatio = 0f;
            tileEntity.SetGlitchedState(false);
        }
   
    }
}