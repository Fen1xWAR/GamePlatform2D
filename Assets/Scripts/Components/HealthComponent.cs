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
        private GameSession _gameSession;

        public void ApllyDamage(int damageValue) // ���������� ����� �����
        {
            _health -= damageValue; // ���������� ����� ���������
            _onDamage?.Invoke(); // ������������� ����� ��� ��������� ������
            if (_health <= 0) // ���� �������� <=0 ��
            {
                _onDie?.Invoke(); // ������������� ����� ��� SMERTI

                _onChange?.Invoke(_health);
            }
        }
        
        public void ApllyHeal(int HealCount)
        {
            if (_health >=20)
            {
                _onChange?.Invoke(_health);
                Debug.Log("Your HP is full");
            }
            else
            {
                _health += HealCount;
                _onHeal?.Invoke();
                _onChange?.Invoke(_health);
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

