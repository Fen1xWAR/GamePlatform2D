using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private UnityEvent _onHeal;

        public void ApllyDamage(int damageValue) // ���������� ����� �����
        {
            _health -= damageValue; // ���������� ����� ���������
            _onDamage?.Invoke(); // ������������� ����� ��� ��������� ������
            if (_health <= 0) // ���� �������� <=0 ��
            {
                _onDie?.Invoke(); // ������������� ����� ��� SMERTI
            }
        }

        public void ApllyHeal(int HealCount)
        {
            _health += HealCount;
            _onHeal?.Invoke();
            if (_health >=20)
            {
                Debug.Log("Your HP is full");
            }
        }
    }
}

