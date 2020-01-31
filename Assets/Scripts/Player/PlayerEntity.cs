using System;
using UnityEngine;

namespace Player
{
    public class PlayerEntity : MonoBehaviour
    {
        private PlayerMovementHandler _movementHandler;

        private void Awake()
        {
            PlayerManager.I.Player = this;
            _movementHandler = GetComponent<PlayerMovementHandler>();
            _movementHandler.Player = this;
        }

        private void Update()
        {
            var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            _movementHandler.MoveInDirection(direction.normalized, direction.magnitude);
        }
    }
}