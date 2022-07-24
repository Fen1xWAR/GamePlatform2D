using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Projectile : BaseProjectile
    {

        protected override void Start()
        {
        base.Start();
        }

        private void FixedUpdate()
        {
            var position = _rigidbody.position;
            position.x += _direction * _flySpeed;
            _rigidbody.MovePosition(position);
        }
    }

}
