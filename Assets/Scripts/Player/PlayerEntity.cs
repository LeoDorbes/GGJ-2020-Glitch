using UnityEngine;

namespace Player
{
    public class PlayerEntity : MonoBehaviour
    {
        private PlayerMovementHandler _movementHandler;
        private TileSwitcherHandler _tileSwitcherHandler;

        private void Awake()
        {
            PlayerManager.I.Player = this;
            _movementHandler = GetComponent<PlayerMovementHandler>();
            _movementHandler.Player = this;
            _tileSwitcherHandler = GetComponent<TileSwitcherHandler>();
        }

        private void Update()
        {
            var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (direction != Vector2.zero)
            {
                _movementHandler.MoveInDirection(direction.normalized, direction.magnitude);
            }
        }
    }
}