using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] P_HPCount player_healthCount;
    [SerializeField] P_HPBar player_healthBar;
    [SerializeField] P_XPBar player_xpBar;
    [SerializeField] P_XPLevel player_xpLevel;
    [SerializeField] AbilityPickerMenu _abilityPickerMenu;

    [Header("Movement")]
    private Vector3 _input;
    [SerializeField] private float _speed = 3.5f;
    private Rigidbody _rb;

    [Header("Shooting")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _shootInterval = 2.0f;
    [SerializeField] private float _shootRadius = 15.0f;
    [SerializeField] public int _projectileCount = 1;
    private float _lastShootTime;

    [Header("Stats")]
    [SerializeField] private float _health = 100f;
    [SerializeField] private float xp_points = 0f;
    [SerializeField] private int xp_level = 1;
    [SerializeField] private string _innateAbilityCode = "arcane_bolt";

    [Header("Abilities")]
    [SerializeField] private bool _IsOrbital;
    [SerializeField] private bool _IsDirect;

    private GameObject[] spirits;
    private float[] angles;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        // Initialize the Spirits system
        InitializeSpirits();

        PostInnateAbility();
        _abilityPickerMenu.SetActive(false);
    }

    void FixedUpdate()
    {
        Movement();
        if (Time.fixedTime > _lastShootTime + _shootInterval)
        {
            Shoot();
            _lastShootTime = Time.fixedTime;
        }
        XPLevel();

        // Update the Spirits system
        if (_IsOrbital)
        {
            UpdateSpirits();
        }
    }

    void Movement()
    {
        _input.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Vector3 newPosition = _rb.position + _input * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);
    }

    void Shoot()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _shootRadius);
        List<Collider> enemies = new List<Collider>();

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                enemies.Add(hitCollider);
            }
        }

        for (int i = 0; i < _projectileCount; i++)
        {
            if (enemies.Count > 0)
            {
                Collider closestEnemy = null;
                float closestDistance = Mathf.Infinity;
                Vector3 playerPosition = transform.position;

                foreach (Collider enemy in enemies)
                {
                    float distanceToPlayer = Vector3.Distance(playerPosition, enemy.transform.position);
                    if (distanceToPlayer < closestDistance)
                    {
                        closestDistance = distanceToPlayer;
                        closestEnemy = enemy;
                    }
                }

                if (closestEnemy != null)
                {
                    enemies.Remove(closestEnemy);
                    GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
                    Projectile projectileScript = projectile.GetComponent<Projectile>();
                    if (_IsDirect)
                    {
                        projectileScript.SetTarget(closestEnemy.transform);
                        projectileScript.SetShootingMode(Projectile.ShootingMode.Direct);
                    }
                    if (_IsOrbital)
                    {
                        projectileScript.SetShootingMode(Projectile.ShootingMode.Orbit);
                    }
                }
            }
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        player_healthCount.UpdateHealth(_health);
        player_healthBar.UpdateHealth(_health);
    }

    public void TakeXP(float xp)
    {
        xp_points += xp;
        UpdateXPUI();
    }

    private void UpdateXPUI()
    {
        player_xpBar.UpdateXP(xp_points);
        player_xpLevel.UpdateXPLevel(xp_level);
    }

    private void XPLevel()
    {
        if (xp_points >= 100)
        {
            xp_points -= 100;
            xp_level++;
            Debug.Log("Ability picker menu opened.");
            _abilityPickerMenu.SetActive(true);
        }
    }

    public void PostInnateAbility()
    {
        _abilityPickerMenu.GetInnateAbility(_innateAbilityCode);
    }

    // Spirits system initialization
    private void InitializeSpirits()
    {
        float angleStep = 360f / _projectileCount;
        spirits = new GameObject[_projectileCount];
        angles = new float[_projectileCount];

        for (int i = 0; i < _projectileCount; i++)
        {
            angles[i] = i * angleStep;
            spirits[i] = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            Projectile spiritScript = spirits[i].GetComponent<Projectile>();
            spiritScript.SetShootingMode(Projectile.ShootingMode.Orbit);
        }
    }

    // Update the position of Spirits
    private void UpdateSpirits()
    {
        for (int i = 0; i < _projectileCount; i++)
        {
            angles[i] += orbitSpeed * Time.deltaTime;
            float x = Mathf.Cos(angles[i] * Mathf.Deg2Rad) * orbitRadius;
            float y = Mathf.Sin(angles[i] * Mathf.Deg2Rad) * orbitRadius;
            spirits[i].transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
        }
    }
}
