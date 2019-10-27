using UnityEngine;

public class TripleProjectile : Projectile
{
    public GameObject splitPrefab;

    public Projectile[] projectiles;

    public float shootForce = 10.0f;

    public int projectileNumber = 3;

    public float fireSpread = 45.0f;

    private bool firstFire = true;

    protected override void Start()
    {
        base.Start();

        projectiles = new Projectile[projectileNumber];
        for (int i = 0; i < projectileNumber; i++)
        {
            GameObject newP = Instantiate(splitPrefab, transform);
            projectiles[i] = newP.GetComponent<Projectile>();
        }
    }
    public override void Fire(Vector3 direction, float force)
    {
        if (firstFire)
        {
            firstFire = false;
        }
        else
        {
            foreach (Projectile projectile in projectiles)
            {
                projectile.ResetProjectile();
            }
        }  

        base.Fire(direction, force);
    }

    public override void ResetProjectile()
    {
        base.ResetProjectile();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Shield" || collision.gameObject.tag == "Target")
        {
            foreach (Projectile projectile in projectiles)
            {
                projectile.transform.parent = null;
            }

            projectiles[0].gameObject.SetActive(true);
            projectiles[0].Fire(Quaternion.Euler(0.0f, fireSpread, 0.0f) * transform.forward, shootForce);

            projectiles[1].gameObject.SetActive(true);
            projectiles[1].Fire(transform.forward, shootForce);

            projectiles[2].gameObject.SetActive(true);
            projectiles[2].Fire(Quaternion.Euler(0.0f, -fireSpread, 0.0f) * transform.forward, shootForce);

            ResetProjectile();
        }
    }
}
