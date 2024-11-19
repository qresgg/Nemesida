using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemyContainer;

    [SerializeField] float _spawnRadius = 10f;
    [SerializeField] float _moveSpeed = 2f;
    [SerializeField] bool _isPlayerDead = false;


    public void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }
    IEnumerator SpawnEnemyRoutine()
    {
            while(!_isPlayerDead)
            {
            Vector2 spawnPoint = Random.insideUnitCircle.normalized * _spawnRadius;
            Vector3 spawnPosition = new Vector3(spawnPoint.x, spawnPoint.y, 0) + new Vector3(_player.transform.position.x, _player.transform.position.y, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(0.1f);
            }
    }
}
