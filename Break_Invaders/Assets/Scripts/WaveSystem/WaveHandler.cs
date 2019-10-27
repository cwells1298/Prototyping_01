using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveHandler : MonoBehaviour
{
    public float maxWaveTime = 30.0f;
    private float currentWaveTime = 0.0f;

    public TextMeshProUGUI turnCounter;
    public TextMeshProUGUI turnTimer;

    private TargetCollection tc;
            
    public static bool waveActive = false;
    private int currentWave = 0;

    private void Start()
    {
        tc = FindObjectOfType<TargetCollection>();
        currentWaveTime = 0.0f;
        StartWave();
    }

    private void ResetWave()
    {
        currentWaveTime = 0.0f;
        waveActive = false;
    }

    private void StartWave()
    {
        currentWave++;
        waveActive = true;
        turnCounter.text = "Current Day: " + currentWave;

        foreach (Target target in tc.targets)
        {
            target.currentHealth = target.maxHealth;
            target.healthSlider.value = target.currentHealth;
            target.isDead = false;
            target.transform.parent.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (waveActive)
        {
            currentWaveTime += Time.deltaTime;

            string timer = "Lunch Timer: " + currentWaveTime.ToString("f2");
            turnTimer.text = timer;

            if (currentWaveTime >= maxWaveTime)
            {
                timer = "Lunch Timer: " + maxWaveTime.ToString("f2");
                turnTimer.text = timer;
                ResetWave();
            }
        }
        else
        {
            if (Input.GetButtonDown("StartWave"))
            {
                StartWave();
            }
        }
    }
}
