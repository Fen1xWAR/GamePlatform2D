using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Character : MonoBehaviour
    {
        private Vector2 _direction;
        private Rigidbody2D _rigidbody;
        private Animator _animator;
      //  private SpriteRenderer _spriteRenderer;
        private bool _isGrounded;
        private bool _allowDoubleJump;
        private Collider2D[] _interactionResult = new Collider2D[1]; // Массив с одним элементом
     //   private int _coins;
       // private CoinValue _coinValue;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunningKey = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");

        [SerializeField] private float _jumpForce;
        [SerializeField] private float _DamageJumpForce;
        [SerializeField] private LayerCheck _groundCheck; //layercheck
        [SerializeField] private float _speed;
        [SerializeField] private bool _doubleJump;
        [SerializeField] private float _interactionRadius; // радиус взаимодействия
        [SerializeField] private LayerMask _interactionLayer; // На каких слоях будет работать
        [SerializeField] private SpawnComponent _footParticles;
        //  [SerializeField] private ParticleSystem _hitParticle;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            // _spriteRenderer = GetComponent<SpriteRenderer>();
           //_coinValue = GetComponent<CoinValue>();
        }
        private void Start()
        {
            bool DoubleJump = _allowDoubleJump;
            DoubleJump = false;
        }
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void FixedUpdate()
        {
            var xVelocity = _direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            _animator.SetBool(IsRunningKey, _direction.x != 0);
            _animator.SetFloat(VerticalVelocity, _rigidbody.velocity.y);
            _animator.SetBool(IsGroundKey, _isGrounded);

            UpdateSpriteDirection();

        }

        private void Update()
        {
            _isGrounded = IsGrounded();
        }

        private float CalculateYVelocity()
        {
            var yVelocity = _rigidbody.velocity.y;
            var isJump = _direction.y > 0;
            if (_doubleJump == true)
            {
                if (_isGrounded) _allowDoubleJump = true;

                if (isJump)
                {
                    yVelocity = CalculateJumpVelocity(yVelocity);
                }
                else if (_rigidbody.velocity.y > 0)
                {
                    yVelocity *= 0.5f;
                }

                return yVelocity;
            }
            else
            {
                if (_isGrounded) _allowDoubleJump = false;

                if (isJump)
                {
                    yVelocity = CalculateJumpVelocity(yVelocity);
                }
                else if (_rigidbody.velocity.y > 0)
                {
                    yVelocity *= 0.5f;
                }

                return yVelocity;
            }

        }

        private float CalculateJumpVelocity(float yVelocity)
        {
            var isFalling = _rigidbody.velocity.y <= 0.001f;
            if (!isFalling) return yVelocity;

            if (_isGrounded)
            {
                yVelocity += _jumpForce;
            }
            else if (_allowDoubleJump)
            {
                yVelocity = _jumpForce;
                _allowDoubleJump = false;
            }

            return yVelocity;
        } // Высчитывание высоту прыжка

       /* public void AddCoins(int Coins) // Добавление монет
        {
            _coins += Coins;
            Debug.Log($"{Coins} coins added. Total coins: {_coins}");
        }*/

        private void UpdateSpriteDirection()
        {
            if (_direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
              //  _spriteRenderer.flipX = false;
            }
            else if (_direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
               // _spriteRenderer.flipX = true;
            }
        } // Поворот по оси Х

        private bool IsGrounded()
        {
            return _groundCheck.IsTouchingLayer;
        }

        public void TakeDamage()
        {
            _animator.SetTrigger(Hit);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _DamageJumpForce); // толчок от дамага по y координате

          //  PlayParticles();
        }

        /* private void OnDrawGizmos() //дебаг
         {
             // Debug.DrawRay(transform.position, Vector3.down, IsGrounded() ? Color.green : Color.red);
             Gizmos.color = IsGrounded() ? Color.green : Color.red;
             Gizmos.DrawSphere(transform.position, 0.3f);
         }*/

        public void Interact() // Создание зоны, в которй персонаж сможет взаимодействовать с предметами
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _interactionRadius,
                _interactionResult,
                _interactionLayer);

            for (int i = 0; i < size; i++) // Перебор массива, если не вернется не один из элементов, то size = 0
            {
                var interactable = _interactionResult[i].GetComponent<InteractableComponent>(); // Получаем компонент
                if (interactable != null) // Если компонент присутствует, то
                {
                    interactable.Interact(); // Выполняется действие
                }
            }
        }
        
        public void SpawnFootPart() // Создание партиклов ходьбы
        {
            _footParticles.Spawn();
        }

    /*    private void PlayParticles()
        {
            _hitParticle.gameObject.SetActive(true);
            _hitParticle.Play();
        } // Проигрываение партиклов у персонажа */
    }
}

