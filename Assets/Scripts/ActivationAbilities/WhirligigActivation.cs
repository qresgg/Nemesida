using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WhirligigActivation : MonoBehaviour
{
    Player _player;
    Whirligig Ability;

    private bool isActive = true;

    private HashSet<Collider> enemiesInTrigger = new HashSet<Collider>();
    private Dictionary<Collider, float> enemyLastHitTime = new Dictionary<Collider, float>();

    void Start()
    {
        Ability = ScriptableObject.CreateInstance<Whirligig>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        transform.localScale = new Vector3(AmplifierController.Instance.RadiusSystem(Ability), 0.01f, AmplifierController.Instance.RadiusSystem(Ability));

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
                    if (!enemyLastHitTime.ContainsKey(enemyCollider) || Time.time - enemyLastHitTime[enemyCollider] >= Ability.Cooldown)
                    {
                        enemiesToDamage.Add(enemyCollider);
                    }
                }
            }

            foreach (var enemyCollider in enemiesToDamage)
            {
                EnemyBase enemy = enemyCollider.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    AmplifierController.Instance.DamageSystem(enemyCollider, Ability);
                    AmplifierController.Instance.PushForceSystem(enemyCollider, Ability, Ability.PushForce, Ability.RecoveryTime, transform.position);
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
        yield return new WaitForSeconds(Ability.Duration);
        isActive = false;
        Destroy(gameObject);
    }
}
