using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Stats : MonoBehaviour
{
    public static P_Stats Instance { get; set; }

    // PROCENTS
    [SerializeField] int MagicDamageAmplifier = 100;
    [SerializeField] int PhysicDamageAmplifier = 100;
    [SerializeField] int PushForceAmplifier = 100;
    [SerializeField] int RadiusAmplifier = 100;

    // NOT PROCENTS
    [SerializeField] int ProjectileCount = 0;
    [SerializeField] int MaxHealthPoints = 100;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public int GetMagicDamageAmplifier()
    {
        return MagicDamageAmplifier / 100;
    }
    public void SetMagicDamageAmplifier(int value)
    {
        MagicDamageAmplifier += value;
    }


    public int GetPhysicDamageAmplifier()
    {
        return PhysicDamageAmplifier / 100;
    }
    public void SetPhysicDamageAmplifier(int value)
    {
        PhysicDamageAmplifier += value;
    }


    public int GetPushForceAmplifier()
    {
        return MagicDamageAmplifier / 100;
    }
    public void SetPushForceAmplifier(int value)
    {
        PushForceAmplifier += value;
    }


    public int GetRadiusAmplifier()
    {
        return RadiusAmplifier / 100;
    }
    public void SetRadiusAmplifier(int value)
    {
       RadiusAmplifier += value;
    }


    public int GetProjectileCount()
    {
        return ProjectileCount;
    }
    public void SetProjectileCount(int value)
    {
        ProjectileCount += value;
    }


    public int GetMaxHealthPoints()
    {
        return MaxHealthPoints;
    }
    public void SetMaxHealthPoints(int value)
    {
        MaxHealthPoints += value;
    }
}
