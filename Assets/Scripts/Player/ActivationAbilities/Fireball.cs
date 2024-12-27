using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballActivation : MonoBehaviour
{
    private Transform target;

    private Fireball Fireball;

    public void Start()
    {
        Fireball = new Fireball();
    }
    private void Update()
    {
        Shoot();
    }
    public void Shoot()
    {
        FindClosestEnemy();
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            this.transform.position += direction * Fireball.ProjectileSpeed * Time.deltaTime;

            //Debug.Log("Рухаюсь до цілі: " + target.name + ", дистанція: " + Vector3.Distance(transform.position, target.position));

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                //Debug.Log("Ціль вражено, об'єкт знищено");
                Destroy(gameObject);
            }
        }
        else
        {
            //Debug.Log("Ціль відсутня або знищена, повторний пошук");
            if (target == null)
            {
                //Debug.Log("Ціль відсутня, об'єкт знищено");
                Destroy(gameObject);
            }
        }
    }

    public void FindClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Fireball.Range);
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
            target = closestEnemy.transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(Fireball.DamageCount);
            Destroy(this.gameObject);
        }
    }
}
