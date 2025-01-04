
using System.Collections;
using System.Reflection;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Player player;
    EnemySpawner enemySpawner;
    XPSpawner xp_spawner;
    [SerializeField] EnemyHealthBar healthBar;

    [Header("Movement")]
    [SerializeField] private float speed = 2.0f;

    [Header("Health")]
    private float health, maxHealth;

    private bool _isPlayerColliding = false;

    [Header("Damage")]
    private int _playerDamage = 2;
    private int _damage = 25;

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
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _isPlayerColliding = true;
            Debug.Log("COLLIDING = TRUE");
            StartCoroutine(HitCoroutine());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("COLLIDING = FALSE");
            _isPlayerColliding = false;
        }
    }

    IEnumerator HitCoroutine()
    {
        while (_isPlayerColliding)
        {
            AttackPlayer(_damage);
            Debug.Log("HIT DETECTED");
            yield return new WaitForSeconds(0.5f);
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
