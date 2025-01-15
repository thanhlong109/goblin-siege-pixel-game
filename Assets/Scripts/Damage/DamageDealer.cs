using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damageAmount = 10f;

    [Header("Hit Settings")]
    public bool destroyOnHit = false; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Damageable target = collision.GetComponent<Damageable>();
        if (target != null)
        {
            target.TakeDamage(damageAmount);

            if (destroyOnHit)
            {
                Destroy(gameObject); 
            }
        }
    }
}
