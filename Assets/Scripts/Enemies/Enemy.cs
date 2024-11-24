
using System.Collections;
using System.Reflection;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player player;
    private EnemySpawner enemySpawner;
    private float speed = 2.0f;
    private float health, maxHealth;
    private Projectile playerProjectile;

    private int _playerDamage = 2;
    private int _damage = 25;

    [SerializeField] EnemyHealthBar healthBar;

    private void Start()
    {
        health = maxHealth;
        player = GameObject.Find("Player").GetComponent<Player>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);

    }
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Projectile")
        {
            TakeDamage(_playerDamage);
            Destroy(other.gameObject);
        }
        if(other.tag == "Player")
        {
            AttackPlayer(_damage);
        }
    }
    
    void Die()
    {
        Destroy(this.gameObject);
    }
    void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }
    public void InitializeHP(int initialHP)
    {
        health = initialHP;
        maxHealth = initialHP;
    }
    public void AttackPlayer(int damage)
    {
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
