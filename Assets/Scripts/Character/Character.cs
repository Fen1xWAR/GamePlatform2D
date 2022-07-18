using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
//using UnityEditor.Animations;
using UnityEngine.Animations;

namespace Scripts
{
    public class Character : Creature
    {
        private AddCoin _addCoin;
        //  private AnimatorController _controller;
        //   private SpriteRenderer _spriteRenderer;
        private bool _allowDoubleJump;
        private readonly Collider2D[] _interactionResult = new Collider2D[1]; // Массив с одним элементом
                                                                              //   [SerializeField] private bool _isArmed;

        [Space]
        [Header("Interaction")]
        [SerializeField] private float _interactionRadius; // радиус взаимодействия
        [SerializeField] private LayerMask _interactionLayer; // На каких слоях будет работать
        [SerializeField] private CheckCircleOverlap _interactionCheck;

        //    [Space] [Header("Particles")] [SerializeField]
        //    private SpawnComponent _footParticles;
        //    [SerializeField] private ParticleSystem _hitParticle;

        [Space]
        [Header("Smth")]
        //   [SerializeField]
        //    private AnimatorController _armed;
        //    [SerializeField] private AnimatorController _disArmed;
        [SerializeField] private float _coinBonus = 1f;

        private SceneManager _scene;
    //    private GameSession _gameSession;

        [Header("Player Stats")]
        public int CurrentCheckpoint;
        public int Coins;
        public int MaxHp = 20;
        public int Hp; // CurrentHP
        public int Damage;

        [Header("Player Level")]
        public int Level;
        public int Xp;
        public int XpToUp;

        [Header("Specs")]
        public int ThrowDamage;
        public bool CanAttack;
        public bool DoubleJump;
        public bool CanThrowAttack;

        [Header("Managment")]
        public string Scene = "Island";
        public float[] Position;

        protected override void Awake()
        {
            base.Awake();
            //  _controller = GetComponent<AnimatorController>();
            // _spriteRenderer = GetComponent<SpriteRenderer>();
            //_coinValue = GetComponent<CoinValue>(); 
            
        }
        private void Start()
        {
    //        _gameSession = FindObjectOfType<GameSession>();
            var health = GetComponent<HealthComponent>();
            health.SetHealth(MaxHp);
            Hp = MaxHp;
            if (File.Exists(Application.persistentDataPath + "/player.nya"))
            {
                LoadPlayer();
            }
            SavePlayer();

            //       UpdateCharWeapon();
        }

        public void OnHeathChanged(int currentHealth)
        {
            Hp = currentHealth;
            if (Hp > MaxHp)
            {
                Hp = MaxHp;
                Debug.Log("Your HP if full!");
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void Update()
        {
            base.Update();

            Scene CurrentScene = SceneManager.GetActiveScene();
            Scene = CurrentScene.name;
        }

        protected override float CalculateYVelocity()
        {
            var isJumpPressing = _direction.y > 0;

            if (_isGrounded)
            {
                _allowDoubleJump = true;
            }



            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if (!_isGrounded && _allowDoubleJump && DoubleJump)
            {
                //    _particles.Spawn("Jump"); // НЕТУ!
                _allowDoubleJump = false;
                DoJumpVfx();
                return _jumpForce;
            }

            return base.CalculateJumpVelocity(yVelocity);
        } // Высчитывание высоту прыжка

        public void AddCoins(int coins) // Добавление монет
        {
            Coins += (int)(coins * _coinBonus);
            Debug.Log($"{(coins * _coinBonus)} coins added. Total coins: {Coins}");
        }
        public void SetCheckpoint(int currentCheckoint)
        {
            CurrentCheckpoint = currentCheckoint;
        }
        protected override void UpdateSpriteDirection()
        {
            base.UpdateSpriteDirection();
        }

        /*  private bool IsGrounded()
          {
              return _groundCheck.IsTouchingLayer;
          }*/

        public override void TakeDamage()
        {
            base.TakeDamage();
        }

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


        /*    private void PlayParticles()
            {
                _hitParticle.gameObject.SetActive(true);
                _hitParticle.Play();
            } // Проигрываение партиклов у персонажа */
        public override void Attack()
        {
            if (!CanAttack) return;
            if (_isGrounded == false) return;
            base.Attack();
        }

        public override void OnDoAttack()
        {
            _damage = Damage;
            base.OnDoAttack();
        }
        public void ArmHero()
        {
            CanAttack = true;
            //        UpdateCharWeapon();
        }

        /*  private void UpdateCharWeapon()
          {
              _animator.runtimeAnimatorController = _gameSession.Data.IsArmed ? _armed : _disArmed; // if (_gameSession.Data.IsArmed) { _animator.runtimeAnimatorController = _armed;} else{_animator.runtimeAnimatorController = _disArmed;}
          }*/

        public void SetBonus()
        {
            _coinBonus = 1.2f;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                this.transform.parent = collision.transform;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                this.transform.parent = null;
            }
        }

        public override void ThrowAttack()
        {
            if (!CanThrowAttack) return;
            if (_isGrounded == false) return;
            base.ThrowAttack();
        }

        public override void OnDoThrowAttack()
        {
            base.OnDoThrowAttack();
        }

        public void SavePlayer()
        {
            SaveSystem.SavePlayer(this);
        }

        public void LoadPlayer()
        {
            PlayerData data = SaveSystem.LoadPlayer();

            CurrentCheckpoint = data.CurrentCheckpoint;
            Coins = data.Coins;
            MaxHp = data.MaxHp;
            Hp = data.Hp;
            Damage = data.Damage;

            Level = data.Level;
            Xp = data.Xp;
            XpToUp = data.XpToUp;

            ThrowDamage = data.ThrowDamage;
            CanAttack = data.CanAttack;
            DoubleJump = data.DoubleJump;
            CanThrowAttack = data.CanThrowAttack;

        //    SceneManager.LoadScene("Island");
        }
    }
}

