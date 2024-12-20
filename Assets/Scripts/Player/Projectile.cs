using UnityEngine;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    private float speed = 10.0f;
    private float shootRadius = 15.0f;

    [System.NonSerialized] public Transform target;
    private Transform player;

    private float orbitRadius = 2.0f;
    private float orbitSpeed = 5.0f;
    private float currentAngle = 0.0f;

    [Header("Abilities")]
    public GameObject[] _abilityPrefabs;
    private List<string> activeAbilities = new List<string>();

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();

        float angleStep = 360f / activeAbilities.Count;
        currentAngle += Mathf.Deg2Rad * angleStep;
    }

    void Update()
    {
    }

    private bool IsAbilityActive(string abilityName)
    {
        return activeAbilities.Contains(abilityName);
    }

    public void AddAbility(string abilityName)
    {
        if (!activeAbilities.Contains(abilityName))
        {
            activeAbilities.Add(abilityName);
        }
    }
}
