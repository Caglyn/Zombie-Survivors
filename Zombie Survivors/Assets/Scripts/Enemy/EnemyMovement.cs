using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _screenBorder;

    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private float _changeDirectionCooldown;
    private Camera _camera;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _targetDirection = transform.up;
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange();
        HandlePlayerTargeting();
        HandleEnemyOffScreen();
    }

    private void HandleRandomDirectionChange()
    {
        _changeDirectionCooldown -= Time.deltaTime;
        
        if(_changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            _targetDirection = rotation * _targetDirection;

            _changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    private void HandlePlayerTargeting()
    {
        if(_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
    }

    private void HandleEnemyOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if((screenPosition.x < _screenBorder && _targetDirection.x < 0) || (screenPosition.x > _camera.pixelWidth - _screenBorder && _targetDirection.x > 0))
        {
            _targetDirection = new Vector2(-_targetDirection.x, _targetDirection.y);
        }

        if((screenPosition.y < _screenBorder && _targetDirection.y < 0) || (screenPosition.y > _camera.pixelHeight - _screenBorder && _targetDirection.y > 0))
        {
            _targetDirection = new Vector2(_targetDirection.x, -_targetDirection.y);
        }
    }

    private void RotateTowardsTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.MoveRotation(rotation);
    }

    private void SetVelocity()
    {
        _rigidbody.velocity = transform.up * _moveSpeed;
    }
}
