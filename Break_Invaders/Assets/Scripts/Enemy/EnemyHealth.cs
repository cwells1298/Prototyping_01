using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 5.0f;

    [SerializeField]
    private int goldPayout = 1;

    private float currentHealth;

    private ScoreSystem sc;

    private EnemyController ec;

    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        sc = FindObjectOfType<ScoreSystem>();

        ec = GetComponent<EnemyController>();

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!WaveHandler.waveActive)
        {
            WaveEnd();
        }
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

        anim.SetTrigger("isHit");

        if (currentHealth <= 0.0f)
        {
            currentHealth = 0.0f;
            ec.firePos.healthSlider.value = currentHealth;
            sc.AddToScore(goldPayout);
            ec.firePos.inUse = false;
            ec.firePos.healthCanvas.SetActive(false);
            FirePosScoreIndicator fpsi = ec.firePos.gameObject.GetComponent<FirePosScoreIndicator>();
            fpsi.TriggerScore(goldPayout);
            Destroy(gameObject);
        }
        else
        {
            ec.firePos.healthSlider.value = currentHealth;
        }
    }

    public void SetDamageUI()
    {
        ec = GetComponent<EnemyController>();
        ec.firePos.healthSlider.maxValue = maxHealth;
        ec.firePos.healthSlider.value = maxHealth;
    }

    private void WaveEnd()
    {
        currentHealth = 0.0f;
        ec.firePos.healthSlider.value = currentHealth;
        ec.firePos.inUse = false;
        ec.firePos.healthCanvas.SetActive(false);
        Destroy(gameObject);
    }
}
