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
    private GameObject actor;

    private void Start()
    {
        currentHealth = maxHealth; 
        animator = GetComponentInParent<Animator>();
        actor = GetComponentInParent<Transform>().gameObject;
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
            Debug.Log("attack");
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

        if (dropItems.Length > 0 && Random.value < dropChance)
        {
            int dropIndex = Random.Range(0, dropItems.Length);
            Instantiate(dropItems[dropIndex], transform.position, Quaternion.identity);
        }

        if (destroyOnDeath)
        {
            Destroy(actor);
        }
    }
}
