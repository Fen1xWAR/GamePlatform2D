using UnityEditor;
using UnityEngine;

namespace Scripts
{
    public class SinusoidalProjectile : BaseProjectile
    {
        private float _originalY;
        [SerializeField] private float _frequency = 1f;
        [SerializeField] private float _amplitude = 1f;
        private float _time;
        protected override void Start()
        {
            base.Start();
            _originalY = _rigidbody.position.y;
        }

        private void FixedUpdate()
        {
            var position = _rigidbody.position;
            position.x += _direction * _flySpeed;
            position.y = _originalY + Mathf.Sin(_time * _frequency) * _amplitude;
            _rigidbody.MovePosition(position);
            _time += Time.fixedDeltaTime;
        }
    }
}