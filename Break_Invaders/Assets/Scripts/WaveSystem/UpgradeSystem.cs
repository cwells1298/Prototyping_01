using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    public List<DestructibleObject> barricades;
    public int barricadeFixCost = 5;

    private ScoreSystem suppliesHolder;

    private bool waveEndTriggered = true;

    private TargetCollection tc;
    public int invulnerabilityBuyCost = 1;

    public GameObject invulnUI;
    public TextMeshProUGUI invulnBuyButtonText;

    private void Start()
    {
        suppliesHolder = FindObjectOfType<ScoreSystem>();
        tc = FindObjectOfType<TargetCollection>();
    }

    private void Update()
    {
        if (!WaveHandler.waveActive && !waveEndTriggered)
        {
            WaveEnd();
        }

        if (WaveHandler.waveActive && waveEndTriggered)
        {
            WaveStart();
        }

    }


    private void WaveEnd()
    {
        waveEndTriggered = true;

        foreach (DestructibleObject item in barricades)
        {
            if(!item.isActive)
            {
                item.upgradeUI.SetActive(true);
                item.upgradeCost.text = "Supplies: " + barricadeFixCost;
            }
        }

        if (!tc.invulnerablilityPowerupAvailable)
        {
            invulnUI.SetActive(true);
            invulnBuyButtonText.text = "Invulnerability Supplies: " + invulnerabilityBuyCost;
        }
    }

    private void WaveStart()
    {
        waveEndTriggered = false;

        foreach (DestructibleObject item in barricades)
        {            
            item.upgradeUI.SetActive(false);
        }

        invulnUI.SetActive(false);
    }

    public void FixBarricade(DestructibleObject barricade)
    {
        if (suppliesHolder.GetGold() >= barricadeFixCost)
        {
            suppliesHolder.SpendGold(barricadeFixCost);

            barricade.upgradeUI.SetActive(false);

            barricade.Activate();
        }
    }

    public void BuyInvulnerability()
    {
        if (suppliesHolder.GetGold() >= invulnerabilityBuyCost)
        {
            suppliesHolder.SpendGold(invulnerabilityBuyCost);

            tc.invulnerablilityPowerupAvailable = true;

            invulnUI.SetActive(false);
        }
    }

}
