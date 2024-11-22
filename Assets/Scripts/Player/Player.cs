using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    private Vector3 _input;
    [SerializeField] private float _speed = 3.5f;
    private Rigidbody _rb;

    [Header("Shooting")]
    [SerializeField] public int _damageOut = 1;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _shootInterval = 2.0f;
    [SerializeField] private float _shootRadius = 15.0f;
    [SerializeField] private float _projectileCount = 1.0f;
    private float _lastShootTime;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        if (Time.fixedTime > _lastShootTime + _shootInterval)
        {
            Shoot();
            _lastShootTime = Time.fixedTime;
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
                    projectileScript.SetTarget(closestEnemy.transform);
                }
            }
        }

    }
}
