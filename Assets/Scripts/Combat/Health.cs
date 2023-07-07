using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public const int maxHealth = 100;

    public int health;
    private bool isInvulnerable;

    public event Action OnTakeDamage;
    public event Action OnDie;

    public bool IsDead => health == 0;


    private void Start()
    {
        health = maxHealth;
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }

    public void DealDamage(int damage)
    {
        if(health == 0) { return; }

        if(isInvulnerable) { return; }

        health = Mathf.Max(health - damage, 0);

        OnTakeDamage?.Invoke();

        if(health == 0)
        {
            OnDie?.Invoke();
        }

        // Debug.Log(health);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            if (health == 0) { return; }

            health = Mathf.Max(health - 15, 0);

            OnTakeDamage?.Invoke();
        }

        if (health == 0)
        {
            OnDie?.Invoke();
        }
    }
}
