using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<EnemyMovement>())
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
