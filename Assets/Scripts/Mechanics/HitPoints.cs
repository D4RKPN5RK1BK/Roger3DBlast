using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour
{
    public float MaxHealth = 1;

    private float _health;

    private bool IsAlive = true;

    public bool AfterDamageInvicability = true;

    private float AfterDamageInvicabilityDuration = 1;

    private bool isInvincable = false;
    private bool endlessInvincibility = false;

    private float invincibilityEndTime;

    void Awake()
    {
        _health = MaxHealth;
        invincibilityEndTime = Time.time;
    }

    void Update()
    {
        if(isInvincable && !endlessInvincibility && Time.time > invincibilityEndTime)
            isInvincable = false;
    }

    public void CallDeath()
    {
        _health = 0;
        IsAlive = false;
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincable)
        {
            _health -= damage;
            if (_health <= 0)
            {
                CallDeath();
            }
            else if (AfterDamageInvicability)
            {
                Invincibility(AfterDamageInvicabilityDuration);
            }
        }
    }

    public void Heal(float health)
    {
        _health = (_health + health > MaxHealth) ? MaxHealth : _health + health;
    }

    public void Heal()
    {
        _health = MaxHealth;
    }

    public void Ressurrect()
    {
        _health = MaxHealth;
        IsAlive = true;
    }

    public void Invincibility(float time)
    {
        isInvincable = true;
        endlessInvincibility = false;
        
        if (Time.time + time > invincibilityEndTime) {
            invincibilityEndTime = Time.time + time;
        }
    }

    public void Invincibility()
    {
        isInvincable = true;
        endlessInvincibility = true;
    }

    public void EndInvincibility()
    {
        isInvincable = false;
    }

    public bool IsInvincable()
    {
        return isInvincable;
    }
}