using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Transform cam;
    public Health Health;

    private void Start()
    {
        SetMaxHealth(Health.maxHealth);
    }

    private void Update()
    {
        SetHealth();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth()
    {
        slider.value = Health.health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
