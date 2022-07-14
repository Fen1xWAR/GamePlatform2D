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
        if (other.gameObject.CompareTag(_tag)) // в случае столкновения с _tag 
        {
            _action?.Invoke(other.gameObject); // вызывается какой-то ивент, заданный в _action
        }
    }

    [Serializable]

    public class EnterEvent : UnityEvent<GameObject> // В этот ивент передаем то, с чем столкнулись
        {
        }
}
