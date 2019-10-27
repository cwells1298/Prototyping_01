using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI goldText, scoreText, finalScoreText;
    [SerializeField]
    private GameObject gameOverScreen;

    private int currentGold;
    private int currentScore;

    public void AddToScore(int scoreIncrease)
    {
        currentGold += scoreIncrease;
        currentScore += scoreIncrease;

        goldText.text = "Supplies: " + currentGold.ToString();
        scoreText.text = "Score: " + currentScore.ToString();
    }

    public void SpendGold(int goldSpent)
    {
        currentGold -= goldSpent;
        goldText.text = "Supplies: " + currentGold.ToString();
    }

    public int GetGold()
    {
        return currentGold;
    }

    public void GameOver()
    {
        finalScoreText.text = "Final Score: " + currentScore.ToString();
        gameOverScreen.SetActive(true);
    }
}
