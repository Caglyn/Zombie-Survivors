using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    private Camera _camera;
    private Animator _animator;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _screenBorder;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        SetPlayerVelocity();
        RotateInDirectionOfMouse();
        SetAnimation();
    }

    private void SetPlayerVelocity()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, _movementInput, ref _movementInputSmoothVelocity, 0.1f);
        _rigidbody.velocity = _smoothedMovementInput * _moveSpeed;

        PreventPlayerGoingOffScreen();
    }

    private void PreventPlayerGoingOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if((screenPosition.x < _screenBorder && _rigidbody.velocity.x < 0) || (screenPosition.x > _camera.pixelWidth - _screenBorder && _rigidbody.velocity.x > 0))
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }

        if((screenPosition.y < _screenBorder && _rigidbody.velocity.y < 0) || (screenPosition.y > _camera.pixelHeight - _screenBorder && _rigidbody.velocity.y > 0))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        }
    }

    private void RotateInDirectionOfMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMouse = mousePosition - transform.position;

        if (directionToMouse != Vector3.zero)
        {
            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

            transform.up = direction;
        }
    }

    private void SetAnimation()
    {
        bool isMoving = _movementInput != Vector2.zero;
        _animator.SetBool("IsMoving", isMoving);
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }
}
