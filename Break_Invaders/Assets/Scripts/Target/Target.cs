using UnityEngine;
using UnityEngine.UI;
public class Target : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 5.0f;

    private float currentHealth;

    public TargetCollection tc;

    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile.activeDamage)
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
            healthSlider.value = currentHealth;

            tc.TargetDestroyed(this);

            Destroy(transform.parent.gameObject);
        }
        else
        {

            healthSlider.value = currentHealth;
        }
    }
}
