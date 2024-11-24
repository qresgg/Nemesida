using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemyContainer;

    [SerializeField] Camera cam;

    [SerializeField] float _spawnRadius = 10f;
    [SerializeField] bool _isPlayerDead = false;
    [SerializeField] int _maxEnemies = 5;
    private int hpIncrease = 0;

    private float minSpawnInterval = 1f;
    private float IntervalSpawn = 10f;
    private float timeSpentInGame = 0f;

    private void Start()
    {
       cam = GameObject.Find("Camera").GetComponent<Camera>();
       StartCoroutine(SpawnEnemyRoutine());
    }
    private void Update()
    {
        timeSpentInGame += Time.deltaTime;
    }
    IEnumerator SpawnEnemyRoutine()
    {
            while(!_isPlayerDead)
            {
            int currentEnemyCount = _enemyContainer.transform.childCount;
                if (currentEnemyCount < _maxEnemies)
                {
                    SpawnEnemy();
                }
            float currentIntervalSpawn = Mathf.Max(minSpawnInterval, IntervalSpawn - timeSpentInGame * 1.1f);
            yield return new WaitForSeconds(currentIntervalSpawn);
        }
    }
    void SpawnEnemy()
    {
        Vector2 spawnPoint = Random.insideUnitCircle.normalized * _spawnRadius;
        Vector3 spawnPosition = new Vector3(spawnPoint.x, spawnPoint.y, 0) + new Vector3(_player.transform.position.x, _player.transform.position.y, 0);
        GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;


        hpIncrease += 1;
        newEnemy.GetComponent<Enemy>().InitializeHP(hpIncrease);
    }
}
