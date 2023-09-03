using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        DestroyWhenOffScreen();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<EnemyMovement>())
        {
            HealthController healthController = other.GetComponent<HealthController>();
            healthController.TakeDamage(10);
            Destroy(gameObject);
        }
    }

    private void DestroyWhenOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if(screenPosition.x < 0 || screenPosition.x > _camera.pixelWidth || screenPosition.y < 0 || screenPosition.y > _camera.pixelHeight)
        {
            Destroy(gameObject);
        }
    }
}
