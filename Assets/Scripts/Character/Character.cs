using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _isGrounded;
    private bool _allowDoubleJump;

    private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
    private static readonly int IsRunningKey = Animator.StringToHash("is-running");
    private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");

    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerCheck _groundCheck;
    [SerializeField] private float _speed;
    [SerializeField] private bool _doubleJump;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); 
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        bool DoubleJump = _allowDoubleJump;
        DoubleJump = false;
    }
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    private void FixedUpdate()
    {
        var xVelocity = _direction.x * _speed;
        var yVelocity = CalculateYVelocity();
        _rigidbody.velocity = new Vector2(xVelocity, yVelocity);
        
        _animator.SetBool(IsRunningKey, _direction.x != 0);
        _animator.SetFloat(VerticalVelocity, _rigidbody.velocity.y);
        _animator.SetBool(IsGroundKey, _isGrounded);

       UpdateSpriteDirection();

    }

    private void Update()
    {
        _isGrounded = IsGrounded();
    }
    
    private float CalculateYVelocity()
    {
        var yVelocity = _rigidbody.velocity.y;
        var isJump = _direction.y > 0;
        if (_doubleJump == true)
        {
            if (_isGrounded) _allowDoubleJump = true;

            if (isJump)
            {
                yVelocity = CalculateJumpVelocity(yVelocity);
            }
            else if (_rigidbody.velocity.y > 0)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }else
        {
            if (_isGrounded) _allowDoubleJump = false;

            if (isJump)
            {
                yVelocity = CalculateJumpVelocity(yVelocity);
            }
            else if (_rigidbody.velocity.y > 0)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }
        
    }

    private float CalculateJumpVelocity(float yVelocity)
    {
        var isFalling = _rigidbody.velocity.y <= 0.001f;
        if (!isFalling) return yVelocity;

        if(_isGrounded)
        {
            yVelocity += _jumpForce;
        }
        else if (_allowDoubleJump)
        {
            yVelocity = _jumpForce;
            _allowDoubleJump = false;
        }

        return yVelocity;
    }

    private void UpdateSpriteDirection()
    {
        if (_direction.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

    private bool IsGrounded()
    {
        return _groundCheck.IsTouchingLayer;                                                               
    }

    /* private void OnDrawGizmos() //дебаг
     {
         // Debug.DrawRay(transform.position, Vector3.down, IsGrounded() ? Color.green : Color.red);
         Gizmos.color = IsGrounded() ? Color.green : Color.red;
         Gizmos.DrawSphere(transform.position, 0.3f);
     }*/
}
