using UnityEngine;
using System.Collections;

public class EnemyWaves : MonoBehaviour
{
    public static EnemyWaves Instance;
    public bool IsBattle { get { return _isBattle; } }

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private int _timeBetweenWaves;

    [SerializeField] private int _minTimeBetweenSpawn;
    [SerializeField] private int _maxTimeBetweenSpawn;

    [SerializeField] private GameObject _warningSign;

    private int _waveNumber = 1;
    private bool _isBattle = false;

    [SerializeField] private float _timeFromLastCheck = 0.3f;

    private void Awake()
    {
        Instance = this;

        LoadValues();
    }

    private void Start()
    {
        Invoke("SpawnEnemyWave", _timeBetweenWaves);
    }

    private void Update()
    {
        if (_timeFromLastCheck <= 0)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) _isBattle = false;
            else _isBattle = true;

            _timeFromLastCheck = 0.3f;
        }

        _timeFromLastCheck -= Time.deltaTime;

        _warningSign.SetActive(_isBattle);
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

        _timeBetweenWaves += 2;
        Invoke("SpawnEnemyWave", _timeBetweenWaves);
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(Random.Range(_minTimeBetweenSpawn, _maxTimeBetweenSpawn));

        var position = GetRandomPosition();
        var enemy = Instantiate(_enemyPrefab, position, Quaternion.identity);
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

    public void SaveValues()
    {
        PlayerPrefs.SetInt("waveNumber", _waveNumber);
    }

    private void LoadValues()
    {
        _waveNumber = PlayerPrefs.GetInt("waveNumber", 1);
    }

    private void OnApplicationQuit()
    {
        SaveValues();
    }
}