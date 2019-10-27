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
}
