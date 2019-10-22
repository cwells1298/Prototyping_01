using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringPosition : MonoBehaviour
{
    public float fireDirection = 0.0f;

    public Queue<EnemyController> enemyQueue;

    public float queueDirection = 0.0f;

    public float queueStaggerDistance = 1.0f;
}
