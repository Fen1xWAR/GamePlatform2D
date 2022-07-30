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
        private readonly Collider2D[] _interactionResult = new Collider2D[1]; // ������ � ����� ���������
                                                                              //   [SerializeField] private bool _isArmed;

        [Space]
        [Header("Interaction")] 
        [SerializeField] private float _interactionRadius; // ������ ��������������
        [SerializeField] private LayerMask _interactionLayer; // �� ����� ����� ����� ��������
        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private LayerCheck _wallCheck;
        //    [Space] [Header("Particles")] [SerializeField]
        //    private SpawnComponent _footParticles;
        //    [SerializeField] private ParticleSystem _hitParticle;
        //    [Space] [Header("Smth")]
        //    [SerializeField]
        //    private AnimatorController _armed;
        //    [SerializeField] private AnimatorController _disArmed;
            private GameSession _gameSession;

        private static readonly int IsOnWallKey = Animator.StringToHash("is-on-wall");
        private float _defaultGravityScale;
        private HealthComponent _healthComponent;


        [Header("Player Stats")]
        public int CurrentCheckpoint;
        public int Coins;
        public int MaxHp = 20;
        public int Hp; // CurrentHP
        public int BaseDamage = 5;
        public int Death = 0;
        public float DamageCoeff = 1;

        [Header("Player Level")]
        public int Level = 1;
        public int Xp = 0;
        public int XpToUp = 100;
        public int AbilPoint = 0;

        [Header("Specs")]
        public int ThrowDamage;
        public bool CanAttack;
        public bool WallSliding;
        public bool DoubleJump = false;
        public bool CanThrowAttack;
        public float CoinBonus = 1f;
        public int CoinLossPercent = 2;

        [Header("Managment")]
        public string Scene = "Island";
        public float[] Position;

        protected override void Awake()
        {
            base.Awake();
            //  _controller = GetComponent<AnimatorController>();
            // _spriteRenderer = GetComponent<SpriteRenderer>();
            //_coinValue = GetComponent<CoinValue>(); 
            _defaultGravityScale = _rigidbody.gravityScale;
            _healthComponent = GetComponent<HealthComponent>();
        }
        private void Start()
        {
    //        _gameSession = FindObjectOfType<GameSession>();
            var health = GetComponent<HealthComponent>();
            health.SetHealth(MaxHp);
            
            if (File.Exists(Application.persistentDataPath + "/player.nya"))
            {
                LoadPlayer();
            }
            SavePlayer();
            Hp = MaxHp;
            //       UpdateCharWeapon();
        }

        public void OnHeathChanged(int currentHealth)
        {
            Hp = currentHealth;
            if (GetComponent<HealthComponent>().Health >= MaxHp)
            {
                GetComponent<HealthComponent>().Health = MaxHp;
                Hp = MaxHp;
                Debug.Log("Your HP if full!");
            }
        }

       

        protected override void FixedUpdate()
        {
            if (_rigidbody.bodyType == RigidbodyType2D.Static)
            {
                return;
            }
            else
            {
                base.FixedUpdate();
            } 
        }

        protected override void Update()
        {
            base.Update();

            Scene CurrentScene = SceneManager.GetActiveScene();
            Scene = CurrentScene.name;

            if (WallSliding == true)
            {
                var moveToSameDirection = _direction.x * transform.lossyScale.x > 0;
                if (_wallCheck.IsTouchingLayer && moveToSameDirection)
                {
                    _isOnWall = true;
                    _rigidbody.gravityScale = 3;
                }
                else
                {
                    _isOnWall = false;
                    _rigidbody.gravityScale = _defaultGravityScale;
                }

                _animator.SetBool(IsOnWallKey, _isOnWall);
            }
            else if (_wallCheck.IsTouchingLayer && WallSliding == false)
            {
                Debug.LogError("Uncorrupted error, please reinstall your project or call an ambulance");
            }

           
        }

        protected override float CalculateYVelocity()
        {
            if (_rigidbody.bodyType == RigidbodyType2D.Static)
            {
                return _rigidbody.velocity.y;
            }
            else
            {
                var isJumpPressing = _direction.y > 0;

                if (_isGrounded || _isOnWall)
                {
                    _allowDoubleJump = true;
                }
                if (!isJumpPressing && _isOnWall)
                {
                    return 0f;
                }
                return base.CalculateYVelocity();
            }
            
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if (!_isGrounded && _allowDoubleJump && DoubleJump && !_isOnWall)
            {
                //    _particles.Spawn("Jump"); // ����!
                _allowDoubleJump = false;
                DoJumpVfx();
                return _jumpForce;
            }

            return base.CalculateJumpVelocity(yVelocity);
        } // ������������ ������ ������

        public void AddCoins(int coins) // ���������� �����
        {
            Coins += (int)(coins * CoinBonus);
            Debug.Log($"{(coins * CoinBonus)} coins added. Total coins: {Coins}");
        }
        public void SetCheckpoint(int currentCheckoint)
        {
            CurrentCheckpoint = currentCheckoint;
        }
        protected override void UpdateSpriteDirection()
        {
            if (_rigidbody.bodyType == RigidbodyType2D.Static)
            {
                return;
            }
            else
            {
                base.UpdateSpriteDirection();
            }        
        }

        /*  private bool IsGrounded()
          {
              return _groundCheck.IsTouchingLayer;
          }*/

        public override void TakeDamage()
        {
            base.TakeDamage();
        }

        public void Interact() // �������� ����, � ������ �������� ������ ����������������� � ����������
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _interactionRadius,
                _interactionResult,
                _interactionLayer);

            for (int i = 0; i < size; i++) // ������� �������, ���� �� �������� �� ���� �� ���������, �� size = 0
            {
                var interactable = _interactionResult[i].GetComponent<InteractableComponent>(); // �������� ���������
                if (interactable != null) // ���� ��������� ������������, ��
                {
                    interactable.Interact(); // ����������� ��������
                }
            }
        }


        /*    private void PlayParticles()
            {
                _hitParticle.gameObject.SetActive(true);
                _hitParticle.Play();
            } // ������������� ��������� � ��������� */
        public override void Attack()
        {
            if (!CanAttack) return;
            if (_isGrounded == false) return;
            base.Attack();
        }
        public int DamageCount()
        {
            var damage = (BaseDamage + Level) * DamageCoeff;
            return (int)damage;
        }
        public override void OnDoAttack()
        {
            _damage = DamageCount();
            base.OnDoAttack();
            Debug.Log(_damage);
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
            CoinBonus = 1.2f;
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
            BaseDamage = data.BaseDamage;
            AbilPoint = data.AbilPoint;
            Death = data.Death;
            DamageCoeff = data.DamageCoeff;

            Level = data.Level;
            Xp = data.Xp;
            XpToUp = data.XpToUp;
            CoinBonus = data.CoinBonus;
            CoinLossPercent = data.CoinLossPercent;

            ThrowDamage = data.ThrowDamage;
            CanAttack = data.CanAttack;
            DoubleJump = data.DoubleJump;
            WallSliding = data.WallSliding;
            CanThrowAttack = data.CanThrowAttack;

        //    SceneManager.LoadScene("Island");
        }

        public void AddXp(int xp)
        {
            Xp += xp;
            XpSystem();
        }
        public void XpSystem()
        {
            if (Level == 1 && Xp >= XpToUp)
            {
                Level = 2;
                Xp = 0;
                XpToUp += 100;
                AbilPoint += 2;
                Debug.Log("Level UP! Your Level is " + Level);
                SavePlayer();
            }
            else if (Level == 2 && Xp >= XpToUp)
            {
                Level = 3;
                Xp = 0;
                XpToUp += 100;
                AbilPoint += 2;
                Debug.Log("Level UP! Your Level is " + Level);
                SavePlayer();
            }
            else return;
        }
        public void DeathUpdate()
        {
            Hp = MaxHp;
            Coins = Coins / 100 + CoinLossPercent;
        }
        public void DeathCount()
        {
            Death++;
        }
    }
}

