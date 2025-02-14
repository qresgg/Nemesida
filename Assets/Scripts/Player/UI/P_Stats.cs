using System.Collections;
using System.Collections.Generic;
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
