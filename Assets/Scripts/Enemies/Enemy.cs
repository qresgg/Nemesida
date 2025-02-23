using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : EnemyBase
{
    [SerializeField] GameObject FloattingTextPrefab;

    [Header("Damage")]
    private int _damage = 25;

    protected override void Initialize()
    {
        base.Initialize();
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    public override void TakeDamage(float damageAmount, string type = "default")
    {
        base.TakeDamage(damageAmount, type);
    }

    public override void AttackPlayer(int damage)
    {
        P_HPController player_hpController = GameObject.Find("HP").GetComponent<P_HPController>();
        if (player_hpController != null && _player != null)
        {
            player_hpController.TakeDamage(damage);
        }
    }
    public override void InitializeHP(int initialHP)
    {
        base.InitializeHP(initialHP);
    }
}
