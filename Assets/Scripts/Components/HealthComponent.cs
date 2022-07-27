using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] public int Health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private HealthChangeEvent _onChange;

        public void ModifyHealth(int healthDelta)
        {
            if (Health <= 0) return;
            Health += healthDelta;
            _onChange?.Invoke(Health);

            if (healthDelta < 0)
            {
                _onDamage?.Invoke();
            }

            if (healthDelta > 0)
            {
                _onHeal?.Invoke();
            }

            if (Health <= 0)
            {
                _onDie?.Invoke();
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Update Health")]
        private void UpdateHealth()
        {
            _onChange?.Invoke(Health);
        }
#endif

        public void SetHealth(int health)
        {
            Health = health;
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int>
        {
        }
    }
}