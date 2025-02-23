using UnityEngine;
using TMPro;
using System.Collections;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Health")]
    protected float health, maxHealth = 100f;
    protected EnemyHealthBar healthBar;

    [Header("Movement")]
    [SerializeField] protected float speed = 2.0f;
    protected bool isPushed = false;
    protected Rigidbody rb;

    [Header("PlayerBehaviour")]
    protected Player _player;
    protected P_HPController player_hpController;
    protected XPSpawner xp_spawner;
    protected int enemyBasicDamage = 25;

    [SerializeField] GameObject FloattingTextPrefab;

    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        player_hpController = GameObject.Find("HP").GetComponent<P_HPController>();
        xp_spawner = GameObject.Find("XPSpawner").GetComponent<XPSpawner>();

        health = maxHealth;

        healthBar.UpdateHealthBar(health, maxHealth);
    }

    protected void MoveTowardsPlayer()
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
    void ShowFloattingText(float damage, string type)
    {
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), -1f);

        var go = Instantiate(FloattingTextPrefab, transform.position + randomOffset, Quaternion.identity, transform);
        go.GetComponent<TextMeshPro>().text = damage.ToString();
        if (type == "default")
        {
            go.GetComponent<TextMeshPro>().color = Color.white;
        }
        else if (type == "critical")
        {
            go.GetComponent<TextMeshPro>().color = Color.yellow;
        }
    }

    public void ApplyImpulseAndRecover(float pushForce, float recoveryTime, Vector3 position)
    {
        Vector3 pushDirection = transform.position - position;
        rb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
        isPushed = true;
        StartCoroutine(RecoverMovement(recoveryTime));
    }

    protected IEnumerator RecoverMovement(float recoveryTime)
    {
        yield return new WaitForSeconds(recoveryTime);
        rb.velocity = Vector3.zero;
        isPushed = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(HitCoroutine());
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
    }

    IEnumerator HitCoroutine()
    {
        while (true)
        {
            AttackPlayer(enemyBasicDamage);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public virtual void TakeDamage(float damageAmount, string type = "default")
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);

        if (FloattingTextPrefab != null)
        {
            ShowFloattingText(damageAmount, type);
        }
    }

    public abstract void AttackPlayer(int damage);

    public virtual void Die()
    {
        Destroy(gameObject);
        
    }

    public virtual void InitializeHP(int initialHP)
    {
        health += initialHP;
        maxHealth += initialHP;
    }
}
