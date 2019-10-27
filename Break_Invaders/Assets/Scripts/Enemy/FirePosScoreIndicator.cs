using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirePosScoreIndicator : MonoBehaviour
{
    public GameObject scoreCanvas;
    public TextMeshProUGUI scoreText;

    public float moveSpeed;

    public float maxDespawnTimer = 2.5f;
    private float currentDespawnTimer = 0.0f;

    private bool isActive = false;
    private Vector3 startPos;

    private float perc = 0.0f;

    private void Start()
    {
        scoreCanvas.SetActive(false);
        startPos = scoreCanvas.transform.position;
        isActive = false;
    }

    public void TriggerScore(int score)
    {
        scoreText.text = "+" + score;
        scoreCanvas.SetActive(true);
        isActive = true;
    }

    public void ResetScoreText()
    {
        currentDespawnTimer = 0.0f;
        scoreCanvas.SetActive(false);
        scoreCanvas.transform.position = startPos;
        isActive = false;
    }


    private void MoveScore()
    {
        scoreCanvas.transform.Translate(0.0f, 0.0f, moveSpeed * Time.deltaTime, Space.World);
    }

    private void ChangeOpacity()
    {
        scoreText.color = new Vector4(scoreText.color.r, scoreText.color.g, scoreText.color.b, 1.0f - perc);
    }

    private void Update()
    {
        if (isActive)
        {
            currentDespawnTimer += Time.deltaTime;
            perc = currentDespawnTimer / maxDespawnTimer;

            if (currentDespawnTimer >= maxDespawnTimer)
            {
                ResetScoreText();
            }
            else
            {
                MoveScore();
                ChangeOpacity();
            }
        }
    }

}
