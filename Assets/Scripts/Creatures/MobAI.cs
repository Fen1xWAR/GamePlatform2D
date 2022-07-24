using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts;

namespace Scripts
{
    public class MobAI : MonoBehaviour
    {
        [Header("Checker")]
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;

        [Header("Cooldowns")]
        [SerializeField] private float _alarmDelay = 0.5f;
        [SerializeField] private float _attackCoolDown = 1f;
        [SerializeField] private float _missHeroCoolDown = 0.5f;

        [Header("Other")]
        [SerializeField] private bool _isTower;
        [SerializeField] private bool _platformPatroling;

        private Coroutine _current;
        private GameObject _target;

        private static readonly int IsDeadKey = Animator.StringToHash("is-dead");

        private SpawnListComponent _particles;
        private Creature _creature;
        private Animator _animator;
        private Patrol _patrol;
        private PlatformPatrolParent _platformPatrol;

        private bool _isDead;
        

        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
            _platformPatrol = GetComponent<PlatformPatrolParent>();
        }

        private void Start()
        {
            if (_isTower) return;
            else 
                StartState(_patrol.DoPatrol());
            
        }

        public void OnHeroInVision(GameObject go)
        {
            if (_isDead)
            {
                return;
            }

            _target = go;

            StartState(AgroToHero());
        }


        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDirectionToTarget();
                }       
                
                yield return null;
            }

            _particles.Spawn("Miss");
            yield return new WaitForSeconds(_missHeroCoolDown);

            StartState(_patrol.DoPatrol());
        }

        private IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCoolDown);
            }

            StartState(GoToHero());
        }

        private void SetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0; // Так как моб двигается только по оси Х (нужно будет создавать новые АИ для летающих и прыгающих мобов)
            _creature.SetDirection(direction.normalized); // Единичный, чтобы не было перепадов по скорости
        }

        private IEnumerator AgroToHero()
        {
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(_alarmDelay);

            StartState(GoToHero());
        }

        private void StartState(IEnumerator coroutine)
        {
            _creature.SetDirection(Vector2.zero);
            if (_current != null) // Если текущая корутина не равно нулю, то
            {
                StopCoroutine(_current); // Останавливаем корутину
            }
            _current = StartCoroutine(coroutine);
        }

        public void OnDie()
        {
            _isDead = true;
            _animator.SetBool(IsDeadKey, true);
            _particles.Spawn("Dead");


            if (_current != null)
            {
                StopCoroutine(_current);
            }
        }

        public void OnHeroInVisionWithoutAgro(GameObject go)
        {
            if (_isDead)
            {
                return;
            }

            _target = go;
        }
    }
}

