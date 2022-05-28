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

        public void ApllyDamage(int damageValue) // ���������� ����� �����
        {
            _health -= damageValue; // ���������� ����� ���������
            _onDamage?.Invoke(); // ������������� ����� ��� ��������� ������
            if (_health <= 0) // ���� �������� <=0 ��
            {
                _onDie?.Invoke(); // ������������� ����� ��� SMERTI
            }
        }
    }
}

