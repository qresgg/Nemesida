using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum ShootingMode
    {
        Direct,
        Orbit
    }

    public ShootingMode currentMode = ShootingMode.Direct;
    private float speed = 10.0f;
    private float shootRadius = 15.0f;

    private Transform target;
    private Transform player;
    private Player _player;

    private float orbitRadius = 2.0f;
    private float orbitSpeed = 5.0f;
    private float currentAngle = 0.0f;

    public int totalProjectiles;
    public int index;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        FindClosestEnemy();

        if (currentMode == ShootingMode.Orbit)
        {
            float angleStep = 360f / totalProjectiles;
            currentAngle += Mathf.Deg2Rad * angleStep * index;
            InvokeRepeating("ToggleActive", 5f, 5f);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (currentMode == ShootingMode.Direct)
        {
            DirectShooting();
        }
        else if (currentMode == ShootingMode.Orbit)
        {
            OrbitShooting();
        }
    }

    void DirectShooting()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OrbitShooting()
    {
        float angleStep = 360f / totalProjectiles;
        currentAngle += Time.deltaTime * orbitSpeed;
        float x = player.position.x + Mathf.Cos(currentAngle) * orbitRadius;
        float y = player.position.y + Mathf.Sin(currentAngle) * orbitRadius;
        Vector3 newPosition = new Vector3(x, y, player.position.z);

        transform.position = newPosition;
    }

    private void ToggleActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void FindClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, shootRadius);
        Collider closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                float distanceToPlayer = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distanceToPlayer < closestDistance)
                {
                    closestDistance = distanceToPlayer;
                    closestEnemy = hitCollider;
                }
            }
        }

        if (closestEnemy != null)
        {
            SetTarget(closestEnemy.transform);
        }
    }

    public void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }

    public void SetShootingMode(ShootingMode mode)
    {
        currentMode = mode;
    }
}
