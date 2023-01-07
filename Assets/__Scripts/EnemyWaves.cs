using UnityEngine;
using System.Collections;

public class EnemyWaves : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private int _timeBetweenWaves;

    [SerializeField] private int _minTimeBetweenSpawn;
    [SerializeField] private int _maxTimeBetweenSpawn;

    private int _waveNumber = 1;

    private void Start()
    {
        Invoke("SpawnEnemyWave", _timeBetweenWaves);
    }

    private void SpawnEnemyWave()
    {
        for (int i = 0; i < _waveNumber + 2; i++)
        {
            StartCoroutine(SpawnEnemy());
        }

        if (_waveNumber % 5 == 0)
        {
            Debug.Log("Boss");
            SpawnBoss();
        }

        _waveNumber++;

        Invoke("SpawnEnemyWave", _timeBetweenWaves);
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(Random.Range(_minTimeBetweenSpawn, _maxTimeBetweenSpawn));

        var position = GetRandomPosition();
        var enemy = Instantiate(_enemyPrefab, position, Quaternion.identity);

        MusicManager.Instance.IsBattle = true;
    }

    private void SpawnBoss()
    {
        var position = GetRandomPosition();

        Instantiate(_bossPrefab, position, Quaternion.identity);
    }

    private Vector2 GetRandomPosition()
    {
        var index = Random.Range(0, _spawnPositions.Length);
        return _spawnPositions[index].position;
    }
}