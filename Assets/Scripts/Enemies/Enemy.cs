
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float speed = 2.0f;
    private Projectile playerProjectile;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Projectile")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
