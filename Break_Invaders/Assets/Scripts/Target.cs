using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 5.0f;

    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            TakeDamage(projectile.GetDamage());
        }
    }

    private void TakeDamage(float dam)
    {
        currentHealth -= dam;
        if (currentHealth <= 0.0f)
        {
            currentHealth = 0.0f;
            //TODO game wide impact e.g. level has list of targets, reduce payout for each target lost, give player temp powerup, lose game when all targets lost

            Destroy(gameObject);
        }
    }
}
