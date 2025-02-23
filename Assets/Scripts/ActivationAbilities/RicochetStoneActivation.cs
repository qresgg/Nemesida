using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetStoneActivation : MonoBehaviour
{
    private Transform target;
    RicochetStone Ability;
    public HashSet<Transform> hitEnemies = new HashSet<Transform>();
    private Transform parentEnemy;
    private bool hasReduced = false; // Flag to track size reduction

    public float currentDamage;

    public void Start()
    {
        Ability = ScriptableObject.CreateInstance<RicochetStone>();
        
        FindRandomOrClosestEnemy();
        currentDamage = Ability.DamageCount;
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
            this.transform.position += direction * Ability.ProjectileSpeed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FindRandomOrClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Ability.Range);
        List<Transform> potentialTargets = new List<Transform>();

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") && !hitEnemies.Contains(hitCollider.transform) && hitCollider.transform != parentEnemy)
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
            if (hitEnemies.Contains(target))
            {
                hitEnemies.Remove(target);
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
            hitEnemies.Add(other.transform);
            if (!hasReduced)
            {
                SpawnSmallerRicochetStones(other.transform);
                hasReduced = true;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void SpawnSmallerRicochetStones(Transform parentEnemy)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Ability.Range / 2);
        List<Transform> potentialTargets = new List<Transform>();

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") && !hitEnemies.Contains(hitCollider.transform) && hitCollider.transform != this.transform)
            {
                potentialTargets.Add(hitCollider.transform);
            }
        }

        int targetsToSpawn = Mathf.Min(potentialTargets.Count, 3);

        for (int i = 0; i < targetsToSpawn; i++)
        {
            Transform newTarget = AssignRandomTarget(potentialTargets);
            potentialTargets.Remove(newTarget);

            GameObject newRicochetStone = Instantiate(this.gameObject);
            newRicochetStone.transform.localScale = this.transform.localScale * 0.5f; 
            var ricochetStoneActivation = newRicochetStone.GetComponent<RicochetStoneActivation>();

            ricochetStoneActivation.target = newTarget;
            ricochetStoneActivation.parentEnemy = parentEnemy;
            ricochetStoneActivation.hitEnemies.Add(newTarget);
            ricochetStoneActivation.hasReduced = true;
            ricochetStoneActivation.currentDamage /= 2;
        }
    }
}
