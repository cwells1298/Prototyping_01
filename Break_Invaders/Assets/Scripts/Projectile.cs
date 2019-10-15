using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class Projectile : MonoBehaviour
{
    public bool inUse = false;

    [SerializeField]
    protected float maxDespawnTime = 20.0f, currentDespawnTime = 0.0f, maxVelocity = 10.0f, damage = 1.0f;

    protected Transform parent;

    [SerializeField]
    protected int currentBounces = 0;
    [SerializeField]
    protected int maxBounces = 10;

    protected Rigidbody rb;
    [SerializeField]
    protected MeshCollider mc;

    public bool hurtEnemy = false;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        mc = GetComponent<MeshCollider>();
        parent = transform.parent;
        ResetProjectile();
        gameObject.SetActive(false);
    }

    protected void Update()
    {
        if (inUse)
        {
            currentDespawnTime += Time.deltaTime;

            //Limit projectile velocity to avoid unwanted behaviour and favour collision detection
            if (rb.velocity.sqrMagnitude != maxVelocity * maxVelocity)
            {
                rb.velocity = rb.velocity.normalized * maxVelocity;
            }

            if (currentDespawnTime >= maxDespawnTime)
            {
                ResetProjectile();
            }
        }
    }

    public virtual void Fire(Vector3 direction, float force)
    {
        inUse = true;
        transform.parent = null;
        mc.enabled = true;
        rb.AddForce(direction * force);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision: " + collision.gameObject.tag);

        if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Shield" || collision.gameObject.tag == "Target")
        {
            currentBounces++;
            hurtEnemy = true;

            if (currentBounces >= maxBounces)
            {
                ResetProjectile();
            }
        }
    }

    public virtual void ResetProjectile()
    {
        currentBounces = 0;
        currentDespawnTime = 0.0f;
        inUse = false;
        mc.enabled = false;
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        hurtEnemy = false;
        gameObject.SetActive(false);
    }

    public float GetDamage()
    {
        return damage;
    }
}
