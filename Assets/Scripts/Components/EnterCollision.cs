using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterCollision : MonoBehaviour
{
    [Header("Tag")]
    [SerializeField] private string _tag;
    [SerializeField] private UnityEvent<GameObject> _action;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(_tag)) // � ������ ������������ � _tag 
        {
            _action?.Invoke(other.gameObject); // ���������� �����-�� �����, �������� � _action
        }
    }

    [Serializable]

    public class EnterEvent : UnityEvent<GameObject> // � ���� ����� �������� ��, � ��� �����������
        {
        }
}
