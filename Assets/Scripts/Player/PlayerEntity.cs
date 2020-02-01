using System;
using Tiles;
using UnityEngine;

namespace Player
{
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private LayerMask _interactionLayerMask;
        
        private PlayerMovementHandler _movementHandler;
        private TileSwitcherHandler _tileSwitcherHandler;

        public bool HasControl = true;

        private PlayerDirectionType _direction = PlayerDirectionType.Down;

        private void Awake()
        {
            PlayerManager.I.Player = this;
            _movementHandler = GetComponent<PlayerMovementHandler>();
            _movementHandler.Player = this;
            _tileSwitcherHandler = GetComponent<TileSwitcherHandler>();
        }

        private void Update()
        {
            if (!HasControl) return;
            
            // Movement
            var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (direction != Vector2.zero)
            {
                _movementHandler.MoveInDirection(direction.normalized, direction.magnitude);
                SetDirectionType(direction);
            }

            // Interaction
            if (Input.GetButtonDown("Jump"))
            {
                var tileEntity = GetTileToInteractWith();

                if (!tileEntity) return;
            }
            
            // Test bug
            if (Input.GetKeyDown(KeyCode.A))
            {
                TileManager.I.addBugs(0.05f);
            }
        }

        private void SetDirectionType(Vector2 direction)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                _direction = direction.x < 0 ? PlayerDirectionType.Left : PlayerDirectionType.Right;
            }
            else
            {
                _direction = direction.y < 0 ? PlayerDirectionType.Down : PlayerDirectionType.Up;
            }
        }

        private TileEntity GetTileToInteractWith()
        {
            var originX = 0f;
            var originY = 0f;

            switch (_direction)
            {
                case PlayerDirectionType.Down:
                    originY = -0.5f;
                    break;
                case PlayerDirectionType.Left:
                    originX = -0.5f;
                    break;
                case PlayerDirectionType.Up:
                    originY = 0.5f;
                    break;
                case PlayerDirectionType.Right:
                    originX = 0.5f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var originFromPlayer = new Vector2(originX, originY);
            var hit = Physics2D.Raycast(
                new Vector2(transform.position.x, transform.position.y) + originFromPlayer,
                originFromPlayer.normalized,
                10,
                _interactionLayerMask
            );

            return hit ? hit.collider.GetComponent<TileEntity>() : null;
        }
    }
}