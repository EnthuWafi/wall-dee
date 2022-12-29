using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //variables
    public List<GameObject> point;

    public List<GameObject> enemy;

    public List<Transform> spawnPosition;

    public float spawnIntervals;

    private PlayerController player;
    private GameManager gameManager;

    public float enemySpawnIntervals;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        /*FUCK THIS METHOD
        InvokeRepeating("SpawnEnemy", enemySpawnIntervals, enemySpawnIntervals);
        InvokeRepeating("SpawnTrash", spawnIntervals, spawnIntervals);*/
    }

    public IEnumerator SpawnPoint()
    {
        while (gameManager.isGameActive)
        {
            yield return new WaitForSeconds(spawnIntervals);

            int randomPos = Random.Range(0, spawnPosition.Count);
            int randomPowerUp = Random.Range(0, point.Count);

            Vector3 spawnPos = spawnPosition[randomPos].position;
            Vector3 offset = Vector3.up * 0.5f;
            //spawning
            Instantiate(point[randomPowerUp], spawnPos + offset, point[randomPowerUp].transform.rotation);
        }

    }
    public IEnumerator SpawnEnemy()
    {
        while (gameManager.isGameActive)
        {
            yield return new WaitForSeconds(enemySpawnIntervals);

            int randomPos = Random.Range(0, spawnPosition.Count);
            int randomEnemy = Random.Range(0, enemy.Count);
            Vector3 spawnPosEnemy = spawnPosition[randomPos].position;
            //spawning
            Instantiate(enemy[randomEnemy], spawnPosEnemy, enemy[randomEnemy].transform.rotation);
        }
    }
}
