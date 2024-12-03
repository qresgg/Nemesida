using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum ShootingMode
    {
        Direct,
        Orbit
    }

    public ShootingMode currentMode = ShootingMode.Direct;
    public float speed = 10.0f;
    private Transform target;
    private Transform player;
    private Player _player;
    public float orbitRadius = 2.0f;
    public float orbitSpeed = 1.0f;
    public int index;
    public int totalProjectiles;
    private float initialAngle;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        float angleStep = 360f / totalProjectiles;
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
        //need to make new method
        float currentAngle = orbitSpeed;
        float x = player.position.x + Mathf.Cos(currentAngle) * orbitRadius;
        float y = player.position.y + Mathf.Sin(currentAngle) * orbitRadius;
        Vector3 newPosition = new Vector3(x, y, player.position.z);

        transform.position = newPosition;
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
