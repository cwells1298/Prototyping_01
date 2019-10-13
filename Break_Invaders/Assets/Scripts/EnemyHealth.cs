using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 5.0f;

    private float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile.hurtEnemy)
            {
                projectile.ResetProjectile();
                TakeDamage(projectile.GetDamage());
            }
        }
    }

    private void TakeDamage(float dam)
    {
        currentHealth -= dam;
        if (currentHealth <= 0.0f)
        {
            currentHealth = 0.0f;

            Destroy(gameObject);
        }
    }
}
