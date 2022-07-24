using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Test_
{
    public class _testMove : MonoBehaviour
    {
        private Rigidbody2D rb;
        private float speed = 0.01f;
        protected Vector2 _direction;

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public void FixedUpdate()
        {
            rb.velocity = new Vector2(rb.velocity.x + speed, rb.velocity.y);
        }
    }
}