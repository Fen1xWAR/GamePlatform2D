using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _tag;

        [SerializeField] private float _alarmDelay;

        private Coroutine _current;
        private GameObject _target;

        private SpawnListComponent _particles;
        private Creature _creature;

        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
        }

        private void Start()
        {
            StartState(Patrooling());
        }

        public void OnHeroInVision(GameObject go)
        {
            _target = go;

            StartState(AgroToHero());
        }

        private IEnumerator Patrooling()
        {
            yield return null;
        }

        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                SetDirectionToTarget();
                yield return null;
            }
        }

        private void SetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0; // Так как моб двигается только по оси Х (нужно будет создавать новые АИ для летающих и прыгающих мобов)
            _creature.SetDirection(direction);
        }

        private IEnumerator AgroToHero()
        {
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(_alarmDelay);

            StartState(GoToHero());
        }

        private void StartState(IEnumerator coroutine)
        {
            if (_current != null) // Если текущая корутина не равно нулю, то
            {
                StopCoroutine(_current); // Останавливаем корутину
            }
            _current = StartCoroutine(coroutine);
        }
    }
}

