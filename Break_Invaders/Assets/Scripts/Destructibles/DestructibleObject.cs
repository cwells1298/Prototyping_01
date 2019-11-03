using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DestructibleObject : MonoBehaviour
{
    public GameObject mainObject;

    public float maxHealth = 2.0f;
    [SerializeField]
    private float currentHealth = 0.0f;

    public GameObject upgradeUI;
    public TextMeshProUGUI upgradeCost;

    public bool isActive = true;

    private void Start()
    {
        if (isActive)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    public void Activate()
    {
        mainObject.SetActive(true);
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float dam)
    {
        currentHealth -= dam;
        if (currentHealth <= 0.0f)
        {
            currentHealth = 0.0f;

            isActive = false;

            Deactivate();
        }
    }

    private void Deactivate()
    {
        mainObject.SetActive(false);
    }
}
