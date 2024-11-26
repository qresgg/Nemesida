using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPSpawner : MonoBehaviour
{
    Player _player;
    EnemySpawner _enemySpawner;
    [SerializeField] GameObject _xpPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CollectDataAndSpawn(Vector3 position)
    {
        Instantiate(_xpPrefab, position, Quaternion.identity);
    }
}
