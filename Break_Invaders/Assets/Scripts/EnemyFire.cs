using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField]
    private GameObject projPrefab;

    private Projectile[] projectiles;
    [SerializeField]
    private int projectileNumber = 3;

    [SerializeField]
    private float shootForce = 10.0f, shootRate = 1.5f;//Once every x seconds

    private float shootCooldown = 0.0f;
    private bool onCooldown = false;

    public EnemyController ec;
    
    // Start is called before the first frame update
    void Start()
    {
        projectiles = new Projectile[projectileNumber];
        for (int i = 0; i < projectileNumber; i++)
        {
            GameObject newP = Instantiate(projPrefab, transform);
            projectiles[i] = newP.GetComponent<Projectile>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!ec.needToMove)
        {
            if (onCooldown)
            {
                shootCooldown += Time.deltaTime;
                if (shootCooldown >= shootRate)
                {
                    onCooldown = false;
                    shootCooldown = 0.0f;
                }
            }
            else
            {
                CheckFireProjectile();
            }
        }   
    }

    private void CheckFireProjectile()
    {
        foreach (Projectile p in projectiles)
        {
            if (!p.inUse)
            {
                onCooldown = true;
                p.gameObject.SetActive(true);
                p.Fire(transform.forward, shootForce);
                return;
            }
        }
    }
}
