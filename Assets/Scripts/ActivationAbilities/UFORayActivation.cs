using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFORayActivation : MonoBehaviour
{
    private Transform target;
    UFORay UFORay;
    private float _lastUFORay;

    Player _player;

    [SerializeField] private GameObject UFORayPrefab;
    private HashSet<Transform> hitEnemies = new HashSet<Transform>();

    private GameObject currentUFORay; 

    private void Start()
    {
        UFORay = new UFORay();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        PickupZone();
        MoveToPlayerPosition();
    }
    void MoveToPlayerPosition()
    {
        transform.position = _player.transform.position;
    }
    private void PickupZone()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, UFORay.Range); // RANGE
        List<Transform> potentialTargets = new List<Transform>();

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") && !hitEnemies.Contains(hitCollider.transform))
            {
                potentialTargets.Add(hitCollider.transform);
            }
        }

        if (potentialTargets.Count > 0)
        {
            target = AssignRandomTarget(potentialTargets);
            RayBehaviour(target);
        }
        else target = null;
    }

    private Transform AssignRandomTarget(List<Transform> potentialTargets)
    {
        int randomIndex = Random.Range(0, potentialTargets.Count);
        return potentialTargets[randomIndex];
    }

    private void RayBehaviour(Transform target)
    {
        if (Time.time > _lastUFORay + UFORay.Cooldown)
        {
            (Vector3 spawnPosition, Quaternion spawnRotation) = RayAdjustments(target);
            currentUFORay = Instantiate(UFORayPrefab, spawnPosition, spawnRotation);

            DestroyRBody(target);
            StartCoroutine(LiftEnemy(target, spawnPosition));

            hitEnemies.Add(target);

            _lastUFORay = Time.time;
        }
    }

    private (Vector3, Quaternion) RayAdjustments(Transform target)
    {
        float playerPositionX = _player.transform.position.x;
        float targetPositionX = target.position.x;

        Vector3 spawnPosition = target.transform.position;
        spawnPosition += (targetPositionX > playerPositionX) ? new Vector3(1.2f, 0, -6.2f) : new Vector3(-1.2f, 0, -6.2f);

        Quaternion spawnRotation = Quaternion.Euler(90, -11, 0);
        spawnRotation = (targetPositionX > playerPositionX) ? Quaternion.Euler(90, -10, 0) : Quaternion.Euler(90, 10, 0);

        return (spawnPosition, spawnRotation);
    }

    private IEnumerator LiftEnemy(Transform enemy, Vector3 beamPosition)
    {
        float liftDuration = 2.0f;
        Vector3 startPosition = enemy.position;
        Vector3 endPosition = beamPosition + new Vector3(0, 0, -11);

        Vector3 liftDirection = (endPosition - startPosition).normalized;
        float elapsedTime = 0;

        float xOffset;
        xOffset = (enemy.position.x > _player.transform.position.x) ? 1.2f : -1.2f;  
        while (elapsedTime < liftDuration)
        {
            float t = elapsedTime / liftDuration;
            Vector3 currentLiftPosition = startPosition + liftDirection * t * (endPosition - startPosition).magnitude;
            currentLiftPosition.x += Mathf.Lerp(0, xOffset, t);
            enemy.position = currentLiftPosition;
            if (enemy.position.z < -11)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.Die();
                Destroy(currentUFORay);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    private void DestroyRBody(Transform target)
    {
        Rigidbody rb = target.GetComponent<Rigidbody>();
        Destroy(rb);
    }
}
