using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemyContainer;

    [SerializeField] float _spawnRadius = 10f;
    [SerializeField] bool _isPlayerDead = false;
    [SerializeField] int _maxEnemies = 5;
    private int hpIncrease = 0;

    public void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }
    public void Update()
    {
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
            yield return new WaitForSeconds(1f);
        }
    }
    void SpawnEnemy()
    {
        hpIncrease += 1;
        Vector2 spawnPoint = Random.insideUnitCircle.normalized * _spawnRadius;
        Vector3 spawnPosition = new Vector3(spawnPoint.x, spawnPoint.y, 0) + new Vector3(_player.transform.position.x, _player.transform.position.y, 0);
        GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
        newEnemy.GetComponent<Enemy>().InitializeHP(hpIncrease);
    }
}
