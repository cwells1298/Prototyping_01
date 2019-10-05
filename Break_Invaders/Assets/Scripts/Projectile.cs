﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class Projectile : MonoBehaviour
{
    public bool inUse = false;

    [SerializeField]
    private float maxDespawnTime = 2.5f;
    private float currentDespawnTime = 0.0f;

    private Transform parent;

    [SerializeField]
    private int currentBounces = 0;
    [SerializeField]
    private int maxBounces = 2;

    private Rigidbody rb;
    private MeshCollider mc;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mc = GetComponent<MeshCollider>();
        parent = transform.parent;
        ResetProjectile();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (inUse)
        {
            currentDespawnTime += Time.deltaTime;

            if (currentDespawnTime >= maxDespawnTime)
            {
                ResetProjectile();
            }
        }
    }

    public void Fire(Vector3 direction, float force)
    {
        inUse = true;
        transform.parent = null;
        mc.enabled = true;
        rb.AddForce(direction * force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision: " + collision.gameObject.tag);

        if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Shield")
        {
            currentBounces++;

            if (currentBounces >= maxBounces)
            {
                ResetProjectile();
            }
        }
    }

    private void ResetProjectile()
    {
        currentBounces = 0;
        currentDespawnTime = 0.0f;
        inUse = false;
        mc.enabled = false;
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
