
using System.Collections;
using System.Reflection;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    private EnemySpawner enemySpawner;
    private float speed = 2.0f;
    private float health, maxHealth;
    private Projectile playerProjectile;

    private int _playerDamage = 2;

    [SerializeField] EnemyHealthBar healthBar;

    private void Start()
    {
        health = maxHealth;
        player = GameObject.Find("Player").GetComponent<Transform>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);

    }
    void Update()
    {
        Vector3 direction = player.position - transform.position;
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
}
