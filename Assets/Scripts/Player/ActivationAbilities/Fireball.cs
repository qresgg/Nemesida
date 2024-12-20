using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballActivation : MonoBehaviour
{
    private Transform target;
    private float speed = 10f;
    private float shootRadius = 15.0f;

    public void Start()
    {
        if (target != null)
        {
            Debug.Log("ֳ�� ��������: " + target.name);
        }
        else
        {
            Debug.Log("ֳ�� �� ��������");
        }
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
            this.transform.position += direction * speed * Time.deltaTime;

            Debug.Log("������� �� ���: " + target.name + ", ���������: " + Vector3.Distance(transform.position, target.position));

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                Debug.Log("ֳ�� �������, ��'��� �������");
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("ֳ�� ������� ��� �������, ��������� �����");
            if (target == null)
            {
                Debug.Log("ֳ�� �������, ��'��� �������");
                Destroy(gameObject);
            }
        }
    }

    public void FindClosestEnemy()
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
            target = closestEnemy.transform;
        }
    }
}
