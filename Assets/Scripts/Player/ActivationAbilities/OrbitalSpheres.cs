using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalSpheresActivation : MonoBehaviour
{
    private Player _player;

    private float speed = 10f;
    private float orbitRadius = 2.0f;
    private float orbitSpeed = 5.0f;
    private float currentAngle = 0.0f;
    private int projectileCount = 1;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        InvokeRepeating("ToggleActive", 5f, 5f);
    }

    private void Update()
    {
        MoveInOrbits();
    }

    public void MoveInOrbits()
    {
        if (_player != null)
        {
            float angleStep = 360f / projectileCount;
            currentAngle += Time.deltaTime * orbitSpeed;
            float x = _player.transform.position.x + Mathf.Cos(currentAngle) * orbitRadius;
            float y = _player.transform.position.y + Mathf.Sin(currentAngle) * orbitRadius;
            Vector3 newPosition = new Vector3(x, y, _player.transform.position.z);

            transform.position = newPosition;
        }
        else
        {
            Debug.LogWarning("Player not assigned. Orbital movement cannot be performed.");
        }
    }

    private void ToggleActive()
    {
        this.gameObject.SetActive(!gameObject.activeSelf);
    }
}
