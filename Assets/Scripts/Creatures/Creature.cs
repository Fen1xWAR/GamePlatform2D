using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Creature : MonoBehaviour
    {
        [Space] [Header("Settings")] [SerializeField]
        private float _speed;
        [SerializeField] protected float _jumpForce;
        [SerializeField] private float _DamageJumpForce;
        [SerializeField] protected int _damage; // ???? ?????????
        [SerializeField] protected bool doubleJump;
        [SerializeField] private string _tagToAttack;
        [SerializeField] private bool _invertScale;

        [Space] [Header("Checkers")] [SerializeField]
        private LayerCheck _groundCheck; //layercheck
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;

        protected Vector2 _direction;
        protected Rigidbody2D _rigidbody;
        protected Animator _animator;
        protected bool _isGrounded;
        private bool _isJumping;
        private bool _isOnWall; // ????????????? ????

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunningKey = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        protected virtual void Update()
        {
            _isGrounded = _groundCheck.IsTouchingLayer;
        }

        protected virtual void FixedUpdate()
        {
            var xVelocity = _direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            _animator.SetBool(IsRunningKey, _direction.x != 0);
            _animator.SetFloat(VerticalVelocity, _rigidbody.velocity.y);
            _animator.SetBool(IsGroundKey, _isGrounded);

            UpdateSpriteDirection();
        }

        protected virtual float CalculateYVelocity()
        {
            var yVelocity = _rigidbody.velocity.y;
            var isJumpPressing = _direction.y > 0;

            if (_isGrounded)
            {
                _isJumping = false;
            }

            if (isJumpPressing) // ???????
            {
                _isJumping = true;

                var isFalling = _rigidbody.velocity.y <= 0.001f;
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
            }
            else if (_rigidbody.velocity.y > 0 && _isJumping)
            {
                yVelocity *= 0.5f;
            }

                return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {
            if (_isGrounded)
            {
                yVelocity = _jumpForce;
             //   _particles.Spawn("Jump");
            }

            return yVelocity;
        } // ???????????? ?????? ??????


        protected virtual void UpdateSpriteDirection()
        {
            var multiplier = _invertScale ? -1 : 1;
            if (_direction.x > 0)
            {
                transform.localScale = new Vector3(multiplier, 1, 1);
                //  _spriteRenderer.flipX = false;
            }
            else if (_direction.x < 0)
            {
                transform.localScale = new Vector3(-1 * multiplier, 1, 1);
                // _spriteRenderer.flipX = true;
            }
        } // ??????? ?? ??? ?
        public virtual void TakeDamage()
        {
            _isJumping = false;
            _animator.SetTrigger(Hit);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _DamageJumpForce); // ?????? ?? ?????? ?? y ??????????
            //  PlayParticles();
        }

        public virtual void Attack()
        {
            _animator.SetTrigger(AttackKey);
        }

        public virtual void OnDoAttack()
        {
            var gos = _attackRange.GetObjectsInRange();
            foreach (var go in gos) // ??????? ????????
            {
                var hp = go.GetComponent<HealthComponent>(); // ??????? ???????? HealthComponent, ? ???????? ? ??????? ??????
                if (hp != null && go.CompareTag(_tagToAttack)) // ???? ???? ???????? ? ??? Enemy
                {
                    hp.ApllyDamage(_damage);
                }
            }
        }
    }
}
