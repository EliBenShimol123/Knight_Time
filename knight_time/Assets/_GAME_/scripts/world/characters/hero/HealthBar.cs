using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider health;

    public void setMaxHealth(float max)
    {
        health.maxValue = max;
        health.value = max;
    }

    public void setHealth(float newHealth)
    {
        health.value = newHealth;
    }
}
