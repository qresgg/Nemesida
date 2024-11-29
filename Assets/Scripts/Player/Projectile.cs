using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10.0f;
    private Transform target;
    private Transform player;
    public float orbitRadius = 2.0f;
    public float orbitSpeed = 1.0f;
    private float currentAngle = 0.0f;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        /*{
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
        }*/
        currentAngle += orbitSpeed;
        float x = player.position.x + Mathf.Cos(currentAngle) * orbitRadius;
        float y = player.position.y + Mathf.Sin(currentAngle) * orbitRadius;
        Vector3 newPosition = new Vector3(x, y, player.position.z);

        transform.position = newPosition;
    }
    public void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
