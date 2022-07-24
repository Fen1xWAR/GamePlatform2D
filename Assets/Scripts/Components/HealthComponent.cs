using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private HealthChangeEvent _onChange;

        public void ModifyHealth(int healthDelta)
        {
            if (_health <= 0) return;

            _health += healthDelta;
            _onChange?.Invoke(_health);

            if (healthDelta < 0)
            {
                _onDamage?.Invoke();
            }

            if (healthDelta > 0)
            {
                _onHeal?.Invoke();
            }

            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Update Health")]
        private void UpdateHealth()
        {
            _onChange?.Invoke(_health);
        }
#endif

        public void SetHealth(int health)
        {
            _health = health;
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int>
        {
        }
    }
}