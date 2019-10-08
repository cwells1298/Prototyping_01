using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool needToMove = true;

    public Vector3 targetPosition;

    public Vector3 startPos;

    public float maxMoveTime = 5.0f;
    [SerializeField]
    private float currentMoveTime = 0.0f;

    private int positionInQueue = 0;

    void Update()
    {
        if (needToMove)
        {         
            float percentage = currentMoveTime / maxMoveTime;
            if (percentage >= 1.0f)
            {
                percentage = 1.0f;
                transform.position = targetPosition;
                needToMove = false;
            }
            else
            {
                Vector3 newPos = Vector3.Lerp(startPos, targetPosition, percentage);
               // Debug.Log(newPos);
                transform.position = newPos;
                currentMoveTime += Time.deltaTime;
            }
        }
    }
}
