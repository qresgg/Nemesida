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
        FindRandomOrClosestEnemy();
    }

    private void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            this.transform.position += direction * Fireball.ProjectileSpeed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

        /* КОД ДЛЯ СТАНА ВЕНГИ - else { //Debug.Log("Ціль відсутня або знищена, повторний пошук"); if (target == null) { //Debug.Log("Ціль відсутня, об'єкт знищено"); Destroy(gameObject); } }*/
    }

    public void FindRandomOrClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Fireball.Range);
        List<Transform> potentialTargets = new List<Transform>();

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                potentialTargets.Add(hitCollider.transform);
            }
        }

        if (potentialTargets.Count > 0)
        {
            int choice = Random.Range(0, 2); // Рандомно вибираємо 0 або 1
            if (choice == 0)
            {
                target = AssignClosestTarget(potentialTargets);
            }
            else
            {
                target = AssignRandomTarget(potentialTargets);
            }
        }
    }

    private Transform AssignClosestTarget(List<Transform> potentialTargets)
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var potentialTarget in potentialTargets)
        {
            float distanceToFireball = Vector3.Distance(transform.position, potentialTarget.position);
            if (distanceToFireball < closestDistance)
            {
                closestDistance = distanceToFireball;
                closestEnemy = potentialTarget;
            }
        }

        return closestEnemy;
    }

    private Transform AssignRandomTarget(List<Transform> potentialTargets)
    {
        int randomIndex = Random.Range(0, potentialTargets.Count);
        return potentialTargets[randomIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(Fireball.DamageCount);
            Destroy(this.gameObject);
        }
    }
}
