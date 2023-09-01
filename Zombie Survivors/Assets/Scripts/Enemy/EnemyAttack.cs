using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damageAmount;

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<PlayerMovement>())
        {
            var healthController = other.gameObject.GetComponent<HealthController>();

            healthController.TakeDamage(_damageAmount);
        }
    }
}
