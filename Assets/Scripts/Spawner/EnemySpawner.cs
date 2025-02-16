using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemyContainer;

    [SerializeField] Camera cam;

    [SerializeField] float _spawnRadius = 16f;
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
        while (!_isPlayerDead)
        {
            int currentEnemyCount = _enemyContainer.transform.childCount;
            if (currentEnemyCount < GameManager.Instance.MaxEnemyCount)
            {
                SpawnEnemy();
            }
            float currentIntervalSpawn = Mathf.Max(minSpawnInterval, IntervalSpawn - timeSpentInGame * 1.1f);
            yield return new WaitForSeconds(currentIntervalSpawn);
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = Vector3.zero;
        int[] sides = new int[] { 0, 1, 2, 3 };
        System.Random rng = new System.Random();
        sides = sides.OrderBy(x => rng.Next()).ToArray();

        foreach (int side in sides)
        {
            float x = 0, y = 0;
            switch (side)
            {
                case 0:
                    x = Random.Range(14f, 19f);
                    y = Random.Range(-10f, 10f);
                    break;
                case 1: 
                    x = Random.Range(-19f, -14f);
                    y = Random.Range(-10f, 10f);
                    break;
                case 2: 
                    x = Random.Range(-14f, 14f);
                    y = Random.Range(10f, 14f);
                    break;
                case 3:
                    x = Random.Range(-14f, 14f);
                    y = Random.Range(-14f, -10f);
                    break;
            }
            spawnPosition = new Vector3(x, y, 0) + _player.transform.position;

            break;
        }

        GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;

        hpIncrease += 1;
        newEnemy.GetComponent<Enemy>().InitializeHP(hpIncrease);
    }
}
