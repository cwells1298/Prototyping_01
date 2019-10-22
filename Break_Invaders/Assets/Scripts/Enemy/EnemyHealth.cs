using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 5.0f;

    [SerializeField]
    private int goldPayout = 1;

    private float currentHealth;

    private ScoreSystem sc;
    void Start()
    {
        currentHealth = maxHealth;
        sc = FindObjectOfType<ScoreSystem>();
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
            sc.AddToScore(goldPayout);
            Destroy(gameObject);
        }
    }
}
