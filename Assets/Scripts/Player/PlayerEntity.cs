using System;
using DG.Tweening;
using Interactibles;
using Tiles;
using UnityEditor.Animations;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private float _interactionRange;
        [SerializeField] private LayerMask _interactionLayerMask;
        [SerializeField] private Animator _animator;

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
            transform.DOScale(Vector3.one, 1.5f);
            transform.DORotate(Vector3.zero, 1.5f);
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
                var interactibleEntity = GetInteractibleToInteractWith();

                if (!interactibleEntity) return;
                interactibleEntity.ChangeGlitchState();
                if (!interactibleEntity.AlreadyInteracted)
                {
                    TileManager.I.addBugs(0.01f);
                    interactibleEntity.AlreadyInteracted = true;
                }
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

        private InteractibleEntity GetInteractibleToInteractWith()
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
                new Vector2(transform.position.x, transform.position.y + 0.5f) + originFromPlayer,
                originFromPlayer.normalized,
                _interactionRange,
                _interactionLayerMask
            );

            return hit ? hit.collider.GetComponentInParent<InteractibleEntity>() : null;
        }
    }
}