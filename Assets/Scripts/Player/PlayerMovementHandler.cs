using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovementHandler : MonoBehaviour
    {
        [SerializeField] private float _speed = 10;
        
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void MoveInDirection(Vector2 direction, float intensity)
        {
            _rigidbody.velocity = direction * intensity * _speed;
        }
    }
}