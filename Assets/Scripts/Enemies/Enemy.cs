using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Player _player;
    EnemySpawner enemySpawner;
    XPSpawner xp_spawner;
    [SerializeField] EnemyHealthBar healthBar;
    Whirligig Whirligig;
    P_HPController player_hpController;

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

        player_hpController = GameObject.Find("HP").GetComponent<P_HPController>();
        _player = GameObject.Find("Player").GetComponent<Player>();
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
            Vector3 direction = _player.transform.position - transform.position;
            direction.Normalize();

            rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
        }
        if (health <= 0)
        {
            Die();
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
    public void Die()
    {
        Destroy(this.gameObject);
        xp_spawner.CollectDataAndSpawn(new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0));
        P_Stats.Instance.EnemyKilled = 1;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    public void InitializeHP(int initialHP)
    {
        health += initialHP;
        maxHealth += initialHP;
    }

    public void AttackPlayer(int damage)
    {
        if (_player != null)
        {
            player_hpController.TakeDamage(damage);
        }
    }
}
