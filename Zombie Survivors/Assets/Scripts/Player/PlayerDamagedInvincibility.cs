using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedInvincibility : MonoBehaviour
{
    [SerializeField] private float _invincibilityDuration;

    private InvincibilityController _invincibilityController;

    private void Awake()
    {
        _invincibilityController = GetComponent<InvincibilityController>();
    }

    public void StartInvincibility()
    {
        _invincibilityController.StartInvincibility(_invincibilityDuration);
    }
}
