using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedPush : MonoBehaviour
{
    public float pushForce = 10.0f;

    public float pushCooldownMax = 5.0f;
    public float pushInProgressTimeMax = 1.0f;

    private bool pushOnCooldown = false;
    public bool pushInProgress = false;
    private float pushCooldownTimer = 0.0f;
    private float pushInProgressTimer = 0.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!pushOnCooldown && !pushInProgress)
        {
            if (Input.GetButtonDown("Push"))
            {
                pushOnCooldown = true;
                pushInProgress = true;

                rb.AddForce(transform.forward * pushForce);
            }
        }
        else
        {
            pushCooldownTimer += Time.deltaTime;

            if (pushCooldownTimer >= pushCooldownMax)
            {
                pushOnCooldown = false;
                pushCooldownTimer = 0.0f;
            }
        }

        if (pushInProgress)
        {
            pushInProgressTimer += Time.deltaTime;

            if (pushInProgressTimer >= pushInProgressTimeMax)
            {
                pushInProgress = false;
                pushInProgressTimer = 0.0f;
            }
        }
    }
}
