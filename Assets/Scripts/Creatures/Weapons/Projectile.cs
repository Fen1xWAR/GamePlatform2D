using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _flySpeed;

        private Rigidbody2D _rigidbody;
        private int _direction;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _direction = transform.lossyScale.x > 0 ? 1 : -1;
        }

        private void FixedUpdate()
        {
            var position = _rigidbody.position;
            position.x += _direction * _flySpeed;
            _rigidbody.MovePosition(position);
        }
    }

}
