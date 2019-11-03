using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetCollection : MonoBehaviour
{
    public List<Target> targets;

    [Range (0.0f, 1.0f)]
    public float failPercentage = 0.0f;

    public bool gameOver = false;

    public int startCount = 0;

    public TextMeshProUGUI targetsLeftText;

    public float targetCount = 0;

    private ScoreSystem sc;
    /*TODO game wide impact e.g. reduce payout for each target lost,
     * give player temp powerup*/

    private float invulnTimeMax = 0.0f;
    private float invulnTimeCurrent = 0.0f;
    private bool isInvuln = false;

    public bool invulnerablilityPowerupAvailable = false;
    private float invulnTimeDefault = 4.0f;

    private void Start()
    {
        foreach (Target target in targets)
        {
            target.tc = this;
        }
        startCount = targets.Count;
        targetCount = startCount;

        sc = FindObjectOfType<ScoreSystem>();

        targetsLeftText.text = "Targets Left: " + targetCount;
    }

    public void TargetDestroyed(Target target)
    {
        //targets.Remove(target);

        targetCount--;

        float percentageLeft = (float)targetCount/ (float)startCount;

        targetsLeftText.text = "Targets Left: " + targetCount;
        if (percentageLeft <= failPercentage)
        {
            gameOver = true;
            sc.GameOver();
        }
    }

    private void Update()
    {
        if (isInvuln)
        {
            invulnTimeCurrent += Time.deltaTime;
            if (invulnTimeCurrent >= invulnTimeMax)
            {
                DeactivateInvulnerability();
            }
        }

        if (invulnerablilityPowerupAvailable && Input.GetButtonDown("ActivatePowerup") && WaveHandler.waveActive)
        {
            ActivateInvulnerability(invulnTimeDefault);
        }
    }

    public void ActivateInvulnerability(float duration)
    {
        isInvuln = true;
        invulnTimeMax = duration;
        invulnTimeCurrent = 0.0f;
        invulnerablilityPowerupAvailable = false;

        foreach (Target item in targets)
        {
            item.isInvulnerable = true;
            item.sliderFill.color = new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
        }
    }

    public void DeactivateInvulnerability()
    {
        isInvuln = false;

        foreach (Target item in targets)
        {
            item.isInvulnerable = false;
            item.sliderFill.color = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
        }
    }
}
