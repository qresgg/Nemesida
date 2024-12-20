using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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
    [SerializeField] private float _shootInterval = 2.0f;
    [SerializeField] public int _projectileCount = 1;
    private float _lastShootTime;

    [Header("Stats")]
    [SerializeField] private float _health = 100f;
    [SerializeField] private float xp_points = 0f;
    [SerializeField] private int xp_level = 1;
    [SerializeField] public string _innateAbilityCode = "arcane_bolt";

    [Header("Abilities")]
    public GameObject[] _abilityPrefabs;
    private List<string> activeAbilities = new List<string> { "fireball" };

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
        for (int i = 0; i < _projectileCount; i++)
        {
            if (IsAbilityActive("fireball"))
            {
                GameObject fireball = Instantiate(_abilityPrefabs[0], transform.position, Quaternion.identity);
                Debug.Log("FIREBALL ADDED");
            }
            if (IsAbilityActive("orbital_spheres"))
            {
                GameObject orbitalSphere = Instantiate(_abilityPrefabs[1], transform.position, Quaternion.identity);
                Debug.Log("ORBITAL SPHERES ADDED");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        UpdateHealthUI();
        if (_health <= 0)
        {
            Death();
        }
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

    private bool IsAbilityActive(string abilityName)
    {
        return activeAbilities.Contains(abilityName);
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
