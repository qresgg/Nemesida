using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class XP : MonoBehaviour
{
    Player _player;
    EnemySpawner _enemySpawner;
    P_XPController player_XPController;

    private bool _isAttractive = false;
    private int xp_points = 25;
    private bool _xpTaken = false;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        player_XPController = GameObject.Find("XP").GetComponent<P_XPController>();
    }
    
    void Update()
    {
        if (_isAttractive && !_xpTaken)
        {
            Vector3 direction = (_player.transform.position - transform.position).normalized;
            this.transform.position += direction * 5 * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickupZone"))
        {
            _isAttractive = true;
        }
        if (other.CompareTag("Player") && !_xpTaken)
        {
            GiveXPToPlayer();
        }
    }

    private void GiveXPToPlayer()
    {
        if (_xpTaken) return;

        //Debug.Log("XP TAKED 1 TIME");
        _xpTaken = true;
        xp_points *= GameManager.Instance.XPMultiplier;
        player_XPController.TakeXP(xp_points);
        Destroy(this.gameObject);
    }
}
