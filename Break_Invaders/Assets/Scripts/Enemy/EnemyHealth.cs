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

    private AudioSource audioSource;

    public AudioClip deathSound;


    public float deathTimeMax = 0.5f;
    public float deathTimecurrent = 0.0f;
    public bool isDying = false;


    void Start()
    {
        currentHealth = maxHealth;
        sc = FindObjectOfType<ScoreSystem>();

        ec = GetComponent<EnemyController>();

        anim = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!WaveHandler.waveActive)
        {
            WaveEnd();
        }

        if (isDying)
        {
            deathTimecurrent += Time.deltaTime;
            if (deathTimecurrent >= deathTimeMax)
            {
                deathTimecurrent = 0.0f;
                isDying = false;
                currentHealth = 0.0f;
                ec.firePos.healthSlider.value = currentHealth;
                sc.AddToScore(goldPayout);
                ec.firePos.inUse = false;
                ec.firePos.healthCanvas.SetActive(false);
                FirePosScoreIndicator fpsi = ec.firePos.gameObject.GetComponent<FirePosScoreIndicator>();
                fpsi.TriggerScore(goldPayout);
                Destroy(gameObject);
            }
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
            isDying = true;
            audioSource.clip = deathSound;
            audioSource.Play(0);
        }
        else
        {
            audioSource.Play(0);
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
        isDying = false;
        Destroy(gameObject);
    }
}
