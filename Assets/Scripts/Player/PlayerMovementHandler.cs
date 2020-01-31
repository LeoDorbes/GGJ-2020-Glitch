using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovementHandler : MonoBehaviour
    {
        [SerializeField] private float _speed = 10;
        
        public PlayerEntity Player { get; set; }
        
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void MoveInDirection(Vector2 direction, float intensity)
        {
            _rigidbody.velocity = direction * intensity * _speed;
        }
    }
}