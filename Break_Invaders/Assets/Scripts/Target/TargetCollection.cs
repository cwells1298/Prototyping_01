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

    private ScoreSystem sc;
    /*TODO game wide impact e.g. reduce payout for each target lost,
     * give player temp powerup*/

    private void Start()
    {
        foreach (Target target in targets)
        {
            target.tc = this;
        }
        startCount = targets.Count;

        sc = FindObjectOfType<ScoreSystem>();

        targetsLeftText.text = "Targets Left: " + targets.Count;
    }

    public void TargetDestroyed(Target target)
    {
        targets.Remove(target);

        float percentageLeft = (float)targets.Count / (float)startCount;

        targetsLeftText.text = "Targets Left: " + targets.Count;
        if (percentageLeft <= failPercentage)
        {
            gameOver = true;
            sc.GameOver();
        }
    }
}
