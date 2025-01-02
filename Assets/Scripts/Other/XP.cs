using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class XP : MonoBehaviour
{
    Player _player;
    EnemySpawner _enemySpawner;

    private bool _isAttractive = false;
    private int xp_points = 25;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    void Update()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized; 
        this.transform.position += direction * 5 * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PickupZone")
        {
            _isAttractive = true;
        }
        if(other.tag == "Player")
        {
            Debug.Log("XP TAKED 1 TIME");
            _player.TakeXP(xp_points);
            Destroy(this.gameObject);
        }
    }
}
