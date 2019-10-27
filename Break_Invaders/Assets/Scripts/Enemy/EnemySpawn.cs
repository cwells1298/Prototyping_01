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
    public int[] enemyPrefabsSpawnWeighting;

    private bool readyToSpawn = true;

    private void SpawnEnemies()
    {
        Transform firePos = GetFiringPosition();

        if (firePos != null)
        {
            int rand = Random.Range(0, enemyPrefabsSpawnWeighting[enemyPrefabsSpawnWeighting.Length - 1]);

            for (int i = 0; i < enemyPrefabs.Length; i++)
            {
                if (rand <= enemyPrefabsSpawnWeighting[i])
                {
                    GameObject newEnemy = Instantiate(enemyPrefabs[i], enemyHolder);

                    EnemyController ec = newEnemy.GetComponent<EnemyController>();
                    EnemyHealth eh = newEnemy.GetComponent<EnemyHealth>();

                    ec.firePos = firePos.GetComponent<FiringPosition>();
                    eh.SetDamageUI();

                    ec.startPos = transform.position;
                    ec.targetPosition = firePos.position;

                    readyToSpawn = false;
                    currentEnemiesSpawned++;

                    break;
                }
            }
        }       
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

    private Transform GetFiringPosition()
    {
        foreach (Transform firePos in firingPositions)
        {
            if (!firePos.GetComponent<FiringPosition>().inUse)
            {
                firePos.GetComponent<FiringPosition>().inUse = true;
                return firePos;
            }
        }

        return null;
    }


}
