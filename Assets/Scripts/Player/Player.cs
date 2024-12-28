using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    [SerializeField] private int _projectileCount;
    private float _lastShootTimeFireball;

    [Header("Stats")]
    [SerializeField] private float _health = 100f;
    [SerializeField] private float xp_points = 0f;
    [SerializeField] private int xp_level = 1;
    [SerializeField] public string _innateAbilityCode = "fireball";

    [Header("Abilities")]
    public GameObject[] _abilityPrefabs;
    private List<string> activeAbilities;

    private Fireball Fireball;
    private OrbitalSpheres OrbitalSpheres;

    [Header("Container")]
    [SerializeField] public GameObject SpheresContainer;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _abilityPickerMenu.SetActive(false);

        activeAbilities = new List<string> { _innateAbilityCode, "orbital_spheres" };

        Fireball = new Fireball();
        OrbitalSpheres = new OrbitalSpheres();

        StartCoroutine(ManageOrbitalSphere());
    }

    void FixedUpdate()
    {
        Movement();
        Shoot();
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
        if (Time.time > _lastShootTimeFireball + _shootInterval)
        {
            if (IsAbilityActive("fireball"))
            {
                for (int i = 0; i < _projectileCount; i++)
                {
                    GameObject fireball = Instantiate(_abilityPrefabs[0], transform.position, Quaternion.identity);
                    Debug.Log("FIREBALL ADDED");
                }
                _lastShootTimeFireball = Time.time;
            }
        }
    }
    IEnumerator ManageOrbitalSphere()
    {
        while (true)
        {
            if (IsAbilityActive("orbital_spheres"))
            {
                for (int i = 0; i < _projectileCount; i++)
                {
                    if (SpheresContainer.transform.childCount < _projectileCount)
                    {
                        GameObject orbitalSphere = Instantiate(_abilityPrefabs[1], transform.position, Quaternion.identity);
                        orbitalSphere.transform.SetParent(SpheresContainer.transform);
                        Debug.Log("ORBITAL SPHERES ADDED");
                    }
                }
                yield return new WaitForSeconds(5);

                ClearChildren(SpheresContainer.transform);

                yield return new WaitForSeconds(5);
            }
        }
    }
    private void ClearChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
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
    public int GetActiveProjectileCount()
    {
        return SpheresContainer.transform.childCount;
    }

}
