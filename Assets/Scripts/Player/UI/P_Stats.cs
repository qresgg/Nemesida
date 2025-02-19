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

    [SerializeField] private int _maxAbilitiesCount = 5;

    // PROCENTS
    [SerializeField] float magicDamageAmplifier = 100;
    [SerializeField] float physicDamageAmplifier = 100;
    [SerializeField] float pushForceAmplifier = 100;
    [SerializeField] float radiusAmplifier = 100;

    // NOT PROCENTS
    [SerializeField] int projectileCount = 0;
    [SerializeField] int maxHealthPoints = 100;
    [SerializeField] float healthRegeneration = 0.25f;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public float MagicDamageAmplifier
    {
        get => magicDamageAmplifier / 100f;
        set => magicDamageAmplifier += value;
    }

    public float PhysicDamageAmplifier
    {
        get => physicDamageAmplifier / 100f;
        set => physicDamageAmplifier += value;
    }

    public float PushForceAmplifier
    {
        get => magicDamageAmplifier / 100f;
        set => magicDamageAmplifier += value;
    }

    public float RadiusAmplifier
    {
        get => radiusAmplifier / 100f;
        set => radiusAmplifier += value;
    }
    public int ProjectileCount
    {
        get => projectileCount;
        set => projectileCount += value;
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
}
