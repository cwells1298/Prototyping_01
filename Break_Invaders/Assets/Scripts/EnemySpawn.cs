using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform enemyHolder;

    public Transform[] firingPositions;

    public float maxSpawnTime = 2.5f;
    private float currentSpawnTime = 0.0f;

    public int maxEnemiesSpawned = 4;
    private int currentEnemiesSpawned = 0;

    public GameObject[] enemyPrefabs;

    private bool readyToSpawn = true;

    private void SpawnEnemies()
    {
        int rand = Random.Range(0, enemyPrefabs.Length);

        int rand2 = Random.Range(0, firingPositions.Length - 1);

        GameObject newEnemy = Instantiate(enemyPrefabs[rand], enemyHolder);
        
        EnemyController ec = newEnemy.GetComponent<EnemyController>();

        ec.startPos = transform.position;
        ec.targetPosition = firingPositions[rand2].position;

        readyToSpawn = false;
        currentEnemiesSpawned++;
    }

    private void Update()
    {
        if (readyToSpawn && currentEnemiesSpawned < maxEnemiesSpawned)
        {
            SpawnEnemies();
        }
        else
        {
            currentSpawnTime += Time.deltaTime;

            if (currentSpawnTime >= maxSpawnTime)
            {
                currentSpawnTime = 0.0f;
                readyToSpawn = true;
            }
        }
    }


}
