using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour
{
    Player _player;
    EnemySpawner _enemySpawner;
    private int xp_points = 25;
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _player.TakeXP(xp_points);
            Destroy(this.gameObject);
        }
    }
}
