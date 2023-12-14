using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private ICollectableBehaviour _collectableBehaviour;

    private void Awake()
    {
        _collectableBehaviour = GetComponent<ICollectableBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();

        if(player != null)
        {
            _collectableBehaviour.OnCollected(player.gameObject);
            Destroy(gameObject);
        }
    }
}
