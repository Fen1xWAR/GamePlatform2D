using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            //Instantiate(_prefab, _target); // 1- ��, ��� �����������, 2-������ � ������� ����� ��������� ������
            var instance = Instantiate(_prefab, _target.position, Quaternion.identity); // 1- ��, ��� �����������, 2- ������� � ���� � ������� ����� ��������� ������, 3 - �������� �� �������
            instance.transform.localScale = _target.lossyScale; // Scale �������� �� ���, ������� � ����������
        }
    }
}
