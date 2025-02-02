using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] P_HPController _hpController;
    [SerializeField] AbilityPickerMenu _abilityPickerMenu;
    [SerializeField] ItemPickerMenu _itemPickerMenu;

    [Header("Movement")]
    private bool leftDirection = false;
    private bool rightDirection = false;
    private Vector3 _input;
    [SerializeField] private float _speed = 3.5f;
    private Rigidbody _rb;

    [Header("Stats")]
    private float _maxHealth = 0;
    private float _currentHealth = 0;

    P_AbilityUser player_abilityUser;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _hpController = GameObject.Find("HP").GetComponent<P_HPController>();
        player_abilityUser = GameObject.Find("P_AbilityUser").GetComponent<P_AbilityUser>();
        _abilityPickerMenu.SetActive(false);
        _itemPickerMenu.SetActive(false);
    }

    void FixedUpdate()
    {
        HealthManager();
        Movement();
        Shoot();
    }

    void Movement()
    {
        _input.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        rightDirection = !(leftDirection = (_input.x > 0 ? false : true));
        Vector3 newPosition = _rb.position + _input * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);
    }
    void HealthManager()
    {
        _currentHealth = _hpController.GetCurrentHP();
        _maxHealth = _hpController.GetMaxHP();
    }
    void Shoot()
    {
        player_abilityUser.ManageFireballActivator();
        player_abilityUser.ManagePlasmaSpheresActivator();
        player_abilityUser.ManageWhirligigActivator();
        player_abilityUser.ManageRicochetStoneActivator();
        player_abilityUser.ManageLaserBeamActivator();
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    public void NewLevelImpulse()
    {
        Collider[] Colliders = Physics.OverlapSphere(transform.position, 5);

        foreach (Collider collider in Colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.ApplyImpulseAndRecover(15f, 0.3f, transform.position);
                }
            }
        }
    }
    public (bool, bool) GetMoveDirection()
    {
        return (leftDirection,  rightDirection);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ItemOrb"))
        {
            GameManager.Instance.OpenItemPickerMenu();
        }
    }
}
