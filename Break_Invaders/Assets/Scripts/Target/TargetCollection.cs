using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollection : MonoBehaviour
{
    public List<Target> targets;

    [Range (0.0f, 1.0f)]
    public float failPercentage = 0.0f;

    public bool gameOver = false;

    public int startCount = 0;

    private ScoreSystem sc;
    /*TODO game wide impact e.g. level has list of targets, reduce payout for each target lost,
     * give player temp powerup, lose game when all targets lost*/

    private void Start()
    {
        foreach (Target target in targets)
        {
            target.tc = this;
        }
        startCount = targets.Count;

        sc = FindObjectOfType<ScoreSystem>();
    }

    public void TargetDestroyed(Target target)
    {
        targets.Remove(target);
        Debug.Log("Targets Left: " + targets.Count);

        float percentageLeft = (float)targets.Count / (float)startCount;
        Debug.Log("Percentage: " + percentageLeft);
        if (percentageLeft <= failPercentage)
        {
            Debug.Log("Game Over - Too Many Targets Destroyed");
            gameOver = true;
            sc.GameOver();
        }
    }
}
