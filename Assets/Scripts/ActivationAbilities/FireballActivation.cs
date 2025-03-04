using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballActivation : MonoBehaviour
{
    private Transform target;
    private Fireball Ability;

    public void Start()
    {
        Ability = ScriptableObject.CreateInstance<Fireball>();
        FindRandomOrClosestEnemy();
    }

    private void FixedUpdate()
    {
        Shoot();
    }

    void Shoot()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            // ��������� ��� ���� �� ���� � ��������
            transform.LookAt(target);
            this.transform.position += direction * Ability.ProjectileSpeed * Time.deltaTime;
            Quaternion rotation = Quaternion.LookRotation(direction); 
            rotation *= Quaternion.Euler(90, 0, 90);
            transform.rotation = rotation;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void FindRandomOrClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Ability.Range);
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
            int choice = Random.Range(0, 2); // rand 0 or 1
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
            AmplifierController.Instance.DamageSystem(other, Ability);
            Destroy(this.gameObject);
        }
    }
}
