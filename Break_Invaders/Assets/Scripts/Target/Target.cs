using UnityEngine;
using UnityEngine.UI;
public class Target : MonoBehaviour
{
    public float maxHealth = 5.0f;

    public float currentHealth;

    public TargetCollection tc;

    public Slider healthSlider;
    public Image sliderFill;

    public bool isDead = false;

    public bool isInvulnerable = false;

    private AudioSource audioSource;

    public float deathTimeMax = 0.5f;
    public float deathTimecurrent = 0.0f;
    public bool isDying = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isDying)
        {
            deathTimecurrent += Time.deltaTime;
            if (deathTimecurrent>=deathTimeMax)
            {
                deathTimecurrent = 0.0f;
                isDying = false;
                currentHealth = 0.0f;
                healthSlider.value = currentHealth;

                tc.TargetDestroyed(this);

                isDead = true;

                transform.parent.gameObject.SetActive(false);
            }
        }
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

                audioSource.Play(0);
            }
        }
    }

    private void TakeDamage(float dam)
    {
        if (!isInvulnerable)
        {
            currentHealth -= dam;

            if (currentHealth <= 0.0f)
            {
                isDying = true;

                //Destroy(transform.parent.gameObject);
            }
            else
            {

                healthSlider.value = currentHealth;
            }
        }
    }
}
