using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

namespace Scripts
{
    public class Character : MonoBehaviour
    {
        private AddCoin _addCoin;
       //  private AnimatorController _controller;
       //  private SpriteRenderer _spriteRenderer;
        private bool _allowDoubleJump;
        private readonly Collider2D[] _interactionResult = new Collider2D[1]; // Массив с одним элементом
                                                                              // private int _coins;
        [Space]
        [Header("Settings")]
        [SerializeField]
        private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _DamageJumpForce;
        [SerializeField] private int _damage; // Урон персонажа
        [SerializeField] private LayerCheck _groundCheck; //layercheck
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] private SpawnListComponent _particles;

        // [SerializeField] private bool _isArmed;

        [Space] [Header("Interaction")]
        [SerializeField] private float _interactionRadius; // радиус взаимодействия
        [SerializeField] private LayerMask _interactionLayer; // На каких слоях будет работать
        
       // [Space] [Header("Particles")] [SerializeField]
       private SpawnComponent _footParticles;
       //  [SerializeField] private ParticleSystem _hitParticle;

        [Space] [Header("Smth")] [SerializeField]
        private AnimatorController _armed;
        [SerializeField] private AnimatorController _disArmed;
        [SerializeField] private float _coinBonus = 1f;

        private GameSession _gameSession;
        private float _defaultGravityScale;

        private Vector2 _direction;
        protected Rigidbody2D _rigidbody;
        private Animator _animator;
        private bool _isGrounded;
        private bool _isJumping;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunningKey = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _defaultGravityScale = _rigidbody.gravityScale;
            //  _controller = GetComponent<AnimatorController>();
            // _spriteRenderer = GetComponent<SpriteRenderer>();
            //_coinValue = GetComponent<CoinValue>();
        }
      
        private void Start()
        {
            bool DoubleJump = _allowDoubleJump;
            DoubleJump = false;
            _gameSession = FindObjectOfType<GameSession>();
            var health = GetComponent<HealthComponent>();

            health.SetHealth(_gameSession.Data.Hp);
            UpdateCharWeapon();
        }

        public void OnHeathChanged(int currentHealth)
        {
            _gameSession.Data.Hp = currentHealth;
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
            if (_gameSession.Data.DoubleJump == true)
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

        public void AddCoins(int coins) // Добавление монет
        {
            _gameSession.Data.Coins += (int)(coins * _coinBonus);
            Debug.Log($"{(coins * _coinBonus)} coins added. Total coins: {_gameSession.Data.Coins}");
        }

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
#if UNITY_EDITOR // Код вырежется при порте на платформу
        private void OnDrawGizmos() //дебаг
         {
             Debug.DrawRay(transform.position, Vector3.down, IsGrounded() ? Color.green : Color.red);
             Gizmos.color = IsGrounded() ? Color.green : Color.red;
             Gizmos.DrawSphere(transform.position, 0.3f);
         }
#endif

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
        public void Attack()
        {
            if (!_gameSession.Data.IsArmed) return;
            _animator.SetTrigger(AttackKey);
        }
        public void OnAttack()
        {
            var gos = _attackRange.GetObjectsInRange();
            foreach (var go in gos) // Перебор объектов
            {
                var hp = go.GetComponent<HealthComponent>(); // Пробуем получить HealthComponent, у объектов в радиусе аттаки
                if (hp != null && go.CompareTag("Enemy")) // Если есть здоровье и тэг Enemy
                {
                    hp.ApllyDamage(_damage);
                }
            }
        }
        public void ArmHero()
        {
            _gameSession.Data.IsArmed = true;
            UpdateCharWeapon();  
        }

        private void UpdateCharWeapon()
        {
            _animator.runtimeAnimatorController = _gameSession.Data.IsArmed ? _armed : _disArmed; // if (_gameSession.Data.IsArmed) { _animator.runtimeAnimatorController = _armed;} else{_animator.runtimeAnimatorController = _disArmed;}
        }

        public void SetBonus()
        {
            _coinBonus = 1.2f;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals ("movingPlatform"))
            {
                 this.transform.parent = collision.transform;
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("movingPlatform"))
            {
                this.transform.parent = null;
            }
        }
    }
}

