using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
   public enum SpawnState { SPAWNING, WAITING, COUNTING};
   
   [System.Serializable]
   public class Wave
    {
        public string name;
        public Enemy enemyPrefab;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBeetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    public TextMeshProUGUI waveIndex;
    public TextMeshProUGUI enemiesLeft;

    private void Start()
    {
        waveCountdown = timeBeetweenWaves;
        WaveCounter();
        enemiesLeft.text = "Enemies left: " + waves[nextWave].count.ToString();
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn point!");
        }
    }

    private void Update()
    {
        if(state == SpawnState.WAITING)
        {
            EnemyCounter();
            if (!EnemyIsAlive())
            {
                WaveClompleted();
            }
            else return;
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                WaveCounter();
                enemiesLeft.text = "Enemies left: " + waves[nextWave].count.ToString();
                StartCoroutine(SpawnWaveCoroutine(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveClompleted()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;
        waveCountdown = timeBeetweenWaves;

        if(nextWave + 1 > waves.Length-1)
        {
            nextWave = 0;
            Debug.Log("Completed all waves!");
            FindObjectOfType<GameManager>().YouWin();
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWaveCoroutine(Wave _wave)
    {
        Debug.Log("Spawnig Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemyPrefab);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Enemy _enemy)
    {
        Debug.Log("Spawning enemy..." + _enemy.name);
        //_enemy.gameObject.SetActive(false);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);

    }

    void WaveCounter()
    {
        waveIndex.text = "Waves left: " + (waves.Length - nextWave).ToString();
    }

    void EnemyCounter()
    {
        enemiesLeft.text = "Enemies left: " + (GameObject.FindGameObjectsWithTag("Enemy").Length).ToString();
    }
}
