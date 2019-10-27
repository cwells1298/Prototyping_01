using UnityEngine;
using UnityEngine.UI;
public class Target : MonoBehaviour
{
    public float maxHealth = 5.0f;

    public float currentHealth;

    public TargetCollection tc;

    public Slider healthSlider;

    public bool isDead = false;

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

            isDead = true;

            transform.parent.gameObject.SetActive(false);

            //Destroy(transform.parent.gameObject);
        }
        else
        {

            healthSlider.value = currentHealth;
        }
    }
}
