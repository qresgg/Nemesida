using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P_Stats : MonoBehaviour
{
    private static P_Stats _instance;
    public static P_Stats Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<P_Stats>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("P_Stats");
                    _instance = obj.AddComponent<P_Stats>();
                }
            }
            return _instance;
        }
    }

    [Header("Counters")]
    [SerializeField] private bool _maxAbilitiesPicked = false;
    [SerializeField] private int _maxAbilitiesCount = 5;
    [SerializeField] private int _enemyKilled = 0;

    // PROCENTS
    [Header("Amplifiers (%)")]
    [SerializeField] float magicDamageAmplifier = 0;
    [SerializeField] float physicDamageAmplifier = 0;

    [SerializeField] float criticalChance = 0;
    [SerializeField] float criticalDamageAmplifier = 0;

    [SerializeField] float pushForceAmplifier = 0;
    [SerializeField] float radiusAmplifier = 0;

    [SerializeField] float xpAmplifier = 0;
    [SerializeField] float speedAmplifier = 0;

    // NOT PROCENTS
    [Header("Amplifiers (default)")]
    [SerializeField] int additionalProjectileCount = 0;
    [Tooltip("Max HP of the player")][SerializeField] int maxHealthPoints = 100;
    [Tooltip("HP regeneration of the player")][SerializeField] float healthRegeneration = 0.25f;

    public event Action<int> OnEnemyKilledChanged;

    public float SpeedAmplifier
    {
        get => speedAmplifier;
        set => speedAmplifier += value;
    }
    public float XPAmplifier
    {
        get => xpAmplifier; 
        set => xpAmplifier += value;
    }
    public float CriticalDamageAmplifier
    {
        get => criticalDamageAmplifier;
        set => criticalDamageAmplifier += value;
    }
    public float CriticalChance
    {
        get => criticalChance;
        set => criticalChance += value;
    }
    public float MagicDamageAmplifier
    {
        get => magicDamageAmplifier;
        set => magicDamageAmplifier += value;
    }

    public float PhysicDamageAmplifier
    {
        get => physicDamageAmplifier;
        set => physicDamageAmplifier += value;
    }

    public float PushForceAmplifier
    {
        get => magicDamageAmplifier;
        set => magicDamageAmplifier += value;
    }

    public float RadiusAmplifier
    {
        get => radiusAmplifier;
        set => radiusAmplifier += value;
    }
    public int ProjectileCount
    {
        get => additionalProjectileCount;
        set => additionalProjectileCount += value;
    }
    public int MaxHealthPoints
    {
        get => maxHealthPoints;
        set => maxHealthPoints += value;
    }

    public float HealthRegeneration
    {
        get => healthRegeneration;
        set => healthRegeneration += value;
    }

    public int MaxAbilitiesCount
    {
        get => _maxAbilitiesCount;
        set => _maxAbilitiesCount += value;
    }
    public int EnemyKilled
    {
        get => _enemyKilled;
        set {
            _enemyKilled += value;
            OnEnemyKilledChanged?.Invoke(_enemyKilled);
        }
    }

    public bool MaxAbilitiesPicked
    {
        get => _maxAbilitiesPicked;
        set => _maxAbilitiesPicked = value;
    }
}
