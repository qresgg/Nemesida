using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class OrbitalSpheresActivation : MonoBehaviour
{
    private Transform target;
    private Projectile _projectile;
    private Player _player;

    private float speed = 10f;

    private float orbitRadius = 2.0f;
    private float orbitSpeed = 5.0f;
    private float currentAngle = 0.0f;

    private int projectileCount = 1; 

    private void Start()
    {
        _projectile = GameObject.Find("Projectile").GetComponent<Projectile>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        target = _projectile.target;

        float angleStep = 360f / projectileCount;
        currentAngle += Mathf.Deg2Rad * angleStep;
    }

    public void Shoot()
    {
        float angleStep = 360f / projectileCount;
        currentAngle += Time.deltaTime * orbitSpeed;
        float x = _player.transform.position.x + Mathf.Cos(currentAngle) * orbitRadius;
        float y = _player.transform.position.y + Mathf.Sin(currentAngle) * orbitRadius;
        Vector3 newPosition = new Vector3(x, y, _player.transform.position.z);

        transform.position = newPosition;
    }
}
