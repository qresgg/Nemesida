using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalSpheresActivation : MonoBehaviour
{
    private Player _player;
    private OrbitalSpheres OrbitalSpheres;

    private float currentAngle = 0.0f;
    private void Start()
    {
        OrbitalSpheres = new OrbitalSpheres();

        _player = GameObject.Find("Player").GetComponent<Player>();
        InvokeRepeating("ToggleActive", OrbitalSpheres.Duration, OrbitalSpheres.Cooldown);
    }

    private void Update()
    {
        MoveInOrbits();
    }

    public void MoveInOrbits()
    {
        if (_player != null)
        {
            float angleStep = 360f / OrbitalSpheres.ProjectileSpeed;
            currentAngle += Time.deltaTime * OrbitalSpheres.ProjectileSpeed;
            float x = _player.transform.position.x + Mathf.Cos(currentAngle) * OrbitalSpheres.Radius;
            float y = _player.transform.position.y + Mathf.Sin(currentAngle) * OrbitalSpheres.Radius;
            Vector3 newPosition = new Vector3(x, y, _player.transform.position.z);

            transform.position = newPosition;
        }
        else
        {
            //Debug.LogWarning("Player not assigned. Orbital movement cannot be performed.");
        }
    }

    private void ToggleActive()
    {
        this.gameObject.SetActive(!gameObject.activeSelf);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(OrbitalSpheres.DamageCount);
        }
    }
}
