using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Scripts
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] private HealthChangeEvent _onChange;

        public void ApllyDamage(int damageValue) // вписывание очков урона
        {
            _health -= damageValue; // списывание очков здоровься
            _onDamage?.Invoke(); // проигрывается ивент при получении дамага
            if (_health <= 0) // если здоровье <=0 то
            {
                _onDie?.Invoke(); // проигрывается ивент при SMERTI

                _onChange?.Invoke(_health);
            }
        }

        public void ApllyHeal(int HealCount)
        {
            _health += HealCount;
            _onHeal?.Invoke();
            if (_health >=20)
            {
                _onChange?.Invoke(_health);
                Debug.Log("Your HP is full");
            }
        }
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

