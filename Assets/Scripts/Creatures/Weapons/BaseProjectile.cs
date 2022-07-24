using UnityEditor;
using UnityEngine;

namespace Scripts
{
    public class BaseProjectile : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] protected float _flySpeed;
        [SerializeField] private bool _invertX;

        protected Rigidbody2D _rigidbody;
        protected int _direction;

        protected virtual void Start()
        {
            var mod = _invertX ? 1 : -1;
            _direction = mod * transform.lossyScale.x > 0 ? 1 : -1;
            _rigidbody = GetComponent<Rigidbody2D>();
            /*var force = new Vector2(_direction * _flySpeed, 0);
            _rigidbody.AddForce(force, ForceMode2D.Impulse);*/
        }
    }
}