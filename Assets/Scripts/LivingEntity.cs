using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth = 1000f;
    public float health { get; private set; }                                                //은닉, 보안성을 위한 프로퍼티
    public bool dead { get; private set; }
    public event Action OnDeath; 

    protected virtual void OnEnable()
    {
        dead = false;
        health = startingHealth;        
    }
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        health -= damage;
        if(health <= 0 && !dead)
        {
            Die();
        }        
    }
    public virtual void Die()
    {
        if(OnDeath != null)
        {
            OnDeath();           
        }
        dead = true;
    }    
}
