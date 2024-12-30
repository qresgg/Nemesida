
using System.Collections;
using System.Reflection;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player player;
    private EnemySpawner enemySpawner;
    private float speed = 2.0f;
    private float health, maxHealth;
    private XPSpawner xp_spawner;


    private int _playerDamage = 2;
    private int _damage = 25;

    [SerializeField] EnemyHealthBar healthBar;

    private void Start()
    {
        maxHealth = 100;
        health = maxHealth;
        player = GameObject.Find("Player").GetComponent<Player>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        xp_spawner = GameObject.Find("XPSpawner").GetComponent<XPSpawner>();
        healthBar.UpdateHealthBar(health, maxHealth);

    }
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            AttackPlayer(_damage);
        }
    }
    
    void Die()
    {
        Destroy(this.gameObject);
        xp_spawner.CollectDataAndSpawn(this.gameObject.transform.position);
    }
    public void TakeDamage(int damageAmount)
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
        health += initialHP;
        maxHealth += initialHP;
    }
    public void AttackPlayer(int damage)
    {
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
