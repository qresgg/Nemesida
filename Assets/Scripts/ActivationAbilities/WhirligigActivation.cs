using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirligigActivation : MonoBehaviour
{
    Whirligig Whirligig;
    Player _player;

    private bool isActive = true;

    private HashSet<Collider> enemiesInTrigger = new HashSet<Collider>();
    private Dictionary<Collider, float> enemyLastHitTime = new Dictionary<Collider, float>();

    void Start()
    {
        Whirligig = new Whirligig();
        _player = GameObject.Find("Player").GetComponent<Player>();
        transform.localScale = new Vector3(Whirligig.Radius, 0.01f, Whirligig.Radius);

        StartCoroutine(DestroyCoroutine());
    }

    private void Update()
    {
        transform.position = _player.transform.position;

        if (isActive)
        {
            List<Collider> enemiesToDamage = new List<Collider>();

            foreach (var enemyCollider in enemiesInTrigger)
            {
                if (enemyCollider != null && enemyCollider.CompareTag("Enemy"))
                {
                    if (!enemyLastHitTime.ContainsKey(enemyCollider) || Time.time - enemyLastHitTime[enemyCollider] >= Whirligig.Cooldown)
                    {
                        enemiesToDamage.Add(enemyCollider);
                    }
                }
            }

            foreach (var enemyCollider in enemiesToDamage)
            {
                Enemy enemy = enemyCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(Whirligig.DamageCount);
                    enemy.ApplyImpulseAndRecover(Whirligig.PushForce, Whirligig.RecoveryTime, transform.position);
                    enemyLastHitTime[enemyCollider] = Time.time;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInTrigger.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInTrigger.Remove(other);
            enemyLastHitTime.Remove(other);
        }
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(Whirligig.Duration);
        isActive = false;
        Destroy(gameObject);
    }
}
