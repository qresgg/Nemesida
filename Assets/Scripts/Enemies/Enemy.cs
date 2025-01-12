using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Player player;
    EnemySpawner enemySpawner;
    XPSpawner xp_spawner;
    [SerializeField] EnemyHealthBar healthBar;
    Whirligig Whirligig;

    [Header("Movement")]
    [SerializeField] private float speed = 2.0f;
    private bool isPushed = false;
    private Rigidbody rb;

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

        rb = GetComponent<Rigidbody>();

        healthBar.UpdateHealthBar(health, maxHealth);

        Whirligig = new Whirligig();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (!isPushed)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();

            rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    public void ApplyImpulseAndRecover(float pushForce, float recoveryTime, Vector3 position)
    {
        Vector3 pushDirection = transform.position - position;
        rb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
        isPushed = true;

        StartCoroutine(RecoverMovement(recoveryTime));
    }

    private IEnumerator RecoverMovement(float recoveryTime)
    {
        yield return new WaitForSeconds(recoveryTime);

        rb.velocity = Vector3.zero;
        isPushed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _isPlayerColliding = true;
            Debug.Log("COLLIDING = TRUE");
            StartCoroutine(HitCoroutine());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
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
