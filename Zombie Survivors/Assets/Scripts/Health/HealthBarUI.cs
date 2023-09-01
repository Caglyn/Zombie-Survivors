using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image _healthBarForegroundImage;

    public void UpdateHealthBar(HealthController healthController)
    {
        _healthBarForegroundImage.fillAmount = healthController.RemainingHealthPercentage();
    }
}
