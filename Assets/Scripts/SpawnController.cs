using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject waveSpawner;
    public GameObject player;
    public GameObject pizzaEnemy;
    public GameObject broccoliEnemy;
    private WaveController currentWave;
    public int wave;
    private int numEnemies;
    private int enemiesAlive;

    void Start()
    {
        wave = 1;
        SetEnemyObjects();
        BeginSpawning();
        EventManager.StartListening(EventNames.ENEMY_DIED, EnemyDied);
    }

    private void SetEnemyObjects() {
        pizzaEnemy.GetComponent<PizzaMovementController>().player = player;
        broccoliEnemy.GetComponent<BroccoliMovementController>().player = player;
    }

    private void BeginSpawning(){
        EventManager.TriggerEvent(EventNames.WAVE_START);

        currentWave = waveSpawner.transform.GetChild(wave).
                            gameObject.GetComponent<WaveController>();
        numEnemies = currentWave.numEnemies;
        SpawnWave(numEnemies);
    }

    private void SpawnWave(int enemiesToSpawn) {
        GameObject spawnPoints = waveSpawner.transform.GetChild(0).gameObject;
        int numSpawners = spawnPoints.transform.childCount;

        foreach(Transform point in spawnPoints.transform) {
            int waveEnemies = enemiesToSpawn / numSpawners;
            int numPizzas = (int) Mathf.Ceil(waveEnemies * currentWave.fractionArePizza);
            int numBroccolis = waveEnemies - numPizzas;
            Debug.Log("Spawning p: " + numPizzas + " b: " + numBroccolis + " at " + point);
            SpawnEnemy(pizzaEnemy, point, numPizzas);
            SpawnEnemy(broccoliEnemy, point, numBroccolis);
        }
    }

    private void CheckWaveFinished() {

    }

    private void SpawnEnemy(GameObject enemy, Transform point, int count) {
        for (int i = 0; i < count; i++) {
            Instantiate(enemy, point);
        }
    }

    private void EnemyDied() {
        numEnemies--;
        CheckWaveFinished();
    }

}
