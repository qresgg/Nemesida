using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] P_HPCount player_healthCount;
    [SerializeField] P_HPBar player_healthBar;
    [SerializeField] P_XPBar player_xpBar;
    [SerializeField] P_XPLevel player_xpLevel;
    [SerializeField] AbilityPickerMenu _abilityPickerMenu;
    GameManager _gameManager;

    [Header("Movement")]
    private Vector3 _input;
    [SerializeField] private float _speed = 3.5f;
    private Rigidbody _rb;

    [Header("Shooting")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _shootInterval = 2.0f;
    [SerializeField] public int _projectileCount = 1;
    private float _lastShootTime;

    [Header("Stats")]
    [SerializeField] private float _health = 100f;
    [SerializeField] private float xp_points = 0f;
    [SerializeField] private int xp_level = 1;
    [SerializeField] public string _innateAbilityCode = "jobm";

    [Header("Abilities")]
    [SerializeField] private bool _IsOrbital;
    [SerializeField] private bool _IsDirect;

    private GameObject[] spirits;
    private float[] angles;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

    }

    void Movement()
    {
        _input.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Vector3 newPosition = _rb.position + _input * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);
    }

    void Shoot()
    {
        if (this.transform.childCount <= _projectileCount - 1)
        {
            for (int i = 0; i < _projectileCount; i++)
            {
                GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
                Projectile projectileScript = projectile.GetComponent<Projectile>();
                projectile.transform.SetParent(transform);
                projectileScript.totalProjectiles = _projectileCount;
                projectileScript.index = i;
                if (_IsDirect)
                {
                    projectileScript.SetShootingMode(Projectile.ShootingMode.Direct);
                }
                if (_IsOrbital)
                {
                    projectileScript.SetShootingMode(Projectile.ShootingMode.Orbit);
                    projectile.tag = "Orbital";
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
            OpenAbilityPickerMenu();
        }
    }

    private void OpenAbilityPickerMenu()
    {
        _abilityPickerMenu.SetActive(true);
        _gameManager.PauseGame();
    }
}
