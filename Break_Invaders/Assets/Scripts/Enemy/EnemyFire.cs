using UnityEngine;
using UnityEngine.UI;

public class EnemyFire : MonoBehaviour
{
    [SerializeField]
    private GameObject projPrefab;

    private Projectile[] projectiles;
    [SerializeField]
    private int projectileNumber = 3;

    [SerializeField]
    private float shootForce = 10.0f, minShootRate = 4.0f, maxShootRate = 6.0f;//Once every x seconds

    private float shootRate = 0.0f, shootCooldown = 0.0f;
    private bool onCooldown = false;

    public EnemyController ec;

    public Image directionMarker;

    public Vector4 baseColour = new Vector4(0.5f, 0.25f, 0.25f, 1.0f);
    public Vector4 fireColour = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
    
    // Start is called before the first frame update
    void Start()
    {
        shootRate = Random.Range(minShootRate, maxShootRate);
        directionMarker.color = fireColour;

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

                float percentage = shootCooldown / shootRate;

                directionMarker.color = Vector4.Lerp(baseColour, fireColour, percentage);

                if (shootCooldown >= shootRate)
                {
                    directionMarker.color = fireColour;
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

                directionMarker.color = baseColour;
                return;
            }
        }
    }
}
