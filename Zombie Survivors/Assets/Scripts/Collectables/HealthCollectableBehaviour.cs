using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectableBehaviour : MonoBehaviour, ICollectableBehaviour
{
    [SerializeField] private float _healthAmount;
    
    public void OnCollected(GameObject player)
    {
        player.GetComponent<HealthController>().AddHealth(_healthAmount);
    }
}
