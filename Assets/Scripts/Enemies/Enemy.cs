
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float speed = 2.0f;
    private float health, maxHealth = 3.0f;
    private Projectile playerProjectile;

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
            TakeDamage(2);
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
}
