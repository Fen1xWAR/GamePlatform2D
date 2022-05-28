using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class DamageComponent : MonoBehaviour
    {
        [SerializeField] private int _DamageValue;

        public void ApplyDamage(GameObject target) // �����, ������� ��������� � ���� ������ � ������� �� �����������
        {
            var healthComponent = target.GetComponent<HealthComponent>(); // �������� ��������� HealthComponent � �������
            if (healthComponent != null) // ��������� ���� �� � ������� ��������� healthComponent, ���� ���� ��
            {
                healthComponent?.ApllyDamage(_DamageValue); // ����������� �����-�� ����
            }
        }
    }
}

