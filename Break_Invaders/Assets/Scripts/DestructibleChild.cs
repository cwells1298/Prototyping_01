using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleChild : MonoBehaviour
{
    public DestructibleObject dObject;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile.activeDamage)
            {
                projectile.ResetProjectile();
                dObject.TakeDamage(projectile.GetDamage());
            }
        }
    }
}
