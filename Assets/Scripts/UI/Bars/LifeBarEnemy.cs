using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class LifeBarEnemy : MonoBehaviour
    {
        [SerializeField] private GameObject _creature;
        [SerializeField] private ProgressBar _healthBar;

        private int _maxHp;
        private int _health;

        private void Start()
        {
            _health = _creature.GetComponent<HealthComponent>().Health;
            _maxHp = _health;
        }

        public void HealthBarUpdate()
        {
            var hpValue = (float)_health/ _maxHp;
            _healthBar.SetProgress(hpValue);
        }
    }
}

