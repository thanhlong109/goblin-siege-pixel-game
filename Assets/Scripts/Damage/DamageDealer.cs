using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] float damageAmount = 10f;
    [SerializeField] LayerMask attackableLayer;

    private PolygonCollider2D polygonCollider2D;
    private Rigidbody2D rb;
    private void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        polygonCollider2D.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.isKinematic = true; 
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent == transform.parent)
        {
            // skip if collision with object same parent
            return;
        }
        // check & call attack function if the collision target is valid
        Debug.Log($"collision {collision.gameObject.name}");
        if (Utility.IsInLayerMask(collision.gameObject, attackableLayer))
        {
            Debug.Log("collision1");
            Damageable target = collision.GetComponent<Damageable>();
            if (target != null)
            {
                Debug.Log("collision2");
                target.TakeDamage(damageAmount);
            }
        }
    }

    // this will be call durring attack animation to enable trigger (collision) 
    public void SetEnableCollider(bool enable)
    {
        polygonCollider2D.enabled = enable;
    }
}
