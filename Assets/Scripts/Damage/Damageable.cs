using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f; 
    private float currentHealth;

    [Header("Death Settings")]
    public bool destroyOnDeath = true;
    public GameObject deathEffect;

    [Header("Drop Settings")]
    public GameObject[] dropItems; 
    public float dropChance = 0.5f;

    [Header("Damage Prevention Settings")]
    public float damagePreventionSeconds = 0.15f;

    private Animator animator;
    private bool canTakeDame = true;

    private void Start()
    {
        currentHealth = maxHealth; 
        animator = GetComponentInParent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (canTakeDame)
        {
            currentHealth -= damage;
            if (animator != null)
            {
                animator.SetBool("takeDameage",true);
            }
            StartCoroutine(DamagePrevention());
        }
       
        
    }

    private IEnumerator DamagePrevention()
    {
        canTakeDame = false;
        yield return new WaitForSeconds(damagePreventionSeconds);
        if (currentHealth > 0)
        {
            canTakeDame = true;
            if(animator != null)
            {
                animator.SetBool("takeDameage", false);
            }
        }
        else
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }

    private void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        if(animator != null)
        {
            animator.SetBool("die", true);
        }

        if (destroyOnDeath)
        {
            Destroy(GetComponentInParent<Transform>().gameObject);
        }
    }
}
