using System;
using System.Collections;
using DG.Tweening;
using Interactibles;
using Tiles;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Player
{
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private float _interactionRange;
        [SerializeField] private LayerMask _interactionLayerMask;
        public Animator _animator;

        private PlayerMovementHandler _movementHandler;
        private TileSwitcherHandler _tileSwitcherHandler;

        public SpriteRenderer ActionHint;

        public bool HasControl = true;

        private PlayerDirectionType _direction = PlayerDirectionType.Down;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            PlayerManager.I.Player = this;
            _movementHandler = GetComponent<PlayerMovementHandler>();
            _movementHandler.Player = this;
            _tileSwitcherHandler = GetComponent<TileSwitcherHandler>();
            Sound.PlaySoundOneShot("event:/SD/SOUND_WARP_IN", transform);
            transform.DOScale(Vector3.one, 1.5f);
            transform.DORotate(Vector3.zero, 1.5f);
        }
        public void ShowActionHint(bool show)
        {
            var targetColor = new Color(1, 1, 1, 0);
            if (show)
            {
                targetColor = new Color(1, 1, 1, 0.8f);
            }
            ActionHint.color = targetColor;
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
            
            var interactibleEntity = GetInteractibleToInteractWith();
            if (!interactibleEntity)
            {
                ShowActionHint(false);
                return;
            }
            
            ShowActionHint(true);

            // Interaction
            if (Input.GetButtonDown("Jump"))
            {
                _animator.SetTrigger("doAction");
                StartCoroutine(GlitchEntity(interactibleEntity));
            }
        }

        private IEnumerator GlitchEntity(InteractibleEntity interactibleEntity)
        {
            HasControl = false;
            yield return new WaitForSeconds(1f);
            interactibleEntity.ChangeGlitchState();
            if (!interactibleEntity.AlreadyInteracted)
            {
                var bugRatio = 0.02f;
                if (SceneManager.GetActiveScene().name == "4")
                {
                    bugRatio = 1f;
                }
                TileManager.I.addBugs(bugRatio);
                GameManager.I.SetBugLevel();
                interactibleEntity.AlreadyInteracted = true;
            }
            yield return new WaitForSeconds(.5f);
            HasControl = true;
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
            var position = transform.position;
            var hit = Physics2D.OverlapCircle(
                new Vector2(position.x, position.y + 0.5f),
                _interactionRange,
                _interactionLayerMask
            );
            
            return hit ? hit.GetComponentInParent<InteractibleEntity>() : null;
        }
    }
}