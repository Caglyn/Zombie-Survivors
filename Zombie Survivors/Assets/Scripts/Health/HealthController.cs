using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maximumHealth;

    public bool IsInvincible { get; set; }

    public UnityEvent OnDeath;
    public UnityEvent OnDamaged;
    public UnityEvent OnHealthChanged;

    public float RemainingHealthPercentage()
    {
        return _currentHealth / _maximumHealth;
    }

    public void TakeDamage(float damage)
    {
        if(_currentHealth == 0)
        {
            return;
        }

        if(IsInvincible)
        {
            return;
        }

        _currentHealth -= damage;
        OnHealthChanged.Invoke();

        if(_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if(_currentHealth == 0)
        {
            OnDeath.Invoke();
        }
        else
        {
            OnDamaged.Invoke();
        }
    }

    public void AddHealth(float heal)
    {
        if(_currentHealth == _maximumHealth)
        {
            return;
        }

        _currentHealth += heal;
        OnHealthChanged.Invoke();

        if(_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }
    }
}
