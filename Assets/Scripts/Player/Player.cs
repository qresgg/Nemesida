using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] P_HPCount player_healthCount;
    [SerializeField] P_HPBar player_healthBar;
    [SerializeField] P_XPBar player_xpBar;
    [SerializeField] P_XPLevel player_xpLevel;
    [SerializeField] AbilityPickerMenu _abilityPickerMenu;

    GameManager _gameManager;
    AbilityUICooldowns _abilityUICooldowns;

    [Header("Movement")]
    private Vector3 _input;
    [SerializeField] private float _speed = 3.5f;
    private Rigidbody _rb;

    [Header("Shooting")]
    [SerializeField] private int _projectileCount;
    private float _lastFireball;
    private float _lastWhirligig;
    float _orbitalSpheresDurationCounter;
    float _orbitalSpheresCooldownCounter;

    [Header("Stats")]
    [SerializeField] private float _health = 100f;
    [SerializeField] private float xp_points = 0f;
    [SerializeField] private int xp_level = 1;

    private Fireball Fireball;
    private OrbitalSpheres OrbitalSpheres;
    private Whirligig Whirligig;

    public bool _isEnemyNearby = false;

    [Header("Container")]
    [SerializeField] public GameObject SpheresContainer;

    P_AbilityUser player_abilityUser;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _abilityUICooldowns = GameObject.Find("Cooldowns").GetComponent<AbilityUICooldowns>();
        player_abilityUser = GameObject.Find("P_AbilityUser").GetComponent<P_AbilityUser>();
        _abilityPickerMenu.SetActive(false);

        Fireball = new Fireball();
        OrbitalSpheres = new OrbitalSpheres();
        Whirligig = new Whirligig();
    }

    void FixedUpdate()
    {
        Movement();
        Shoot();
    }

    void Movement()
    {
        _input.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Vector3 newPosition = _rb.position + _input * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);
    }

    void Shoot()
    {
        player_abilityUser.ManageFireballActivator();
        player_abilityUser.ManageOrbitalSphereActivator();
        player_abilityUser.ManageWhirligigActivator();
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
        XPLevel();
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

    public void Death()
    {
        Destroy(gameObject);
    }
    public int GetActiveProjectileCount()
    {
        return SpheresContainer.transform.childCount;
    }

}
