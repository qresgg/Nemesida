using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplifierController : MonoBehaviour
{
    private static AmplifierController _instance;
    public static AmplifierController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AmplifierController>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("AmplifierController");
                    _instance = obj.AddComponent<AmplifierController>();
                }
            }
            return _instance;
        }
    }

    public void DamageSystem(Collider target, AbilityBase Ability)
    {
        if (Ability.DamageType == "Magical")
        {
            float damage = (Ability.DamageCount + (Ability.DamageCount * P_Stats.Instance.MagicDamageAmplifier)) * GameManager.Instance.DamageMultiplier;
            target.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log(damage);
        }
        if (Ability.DamageType == "Physical")
        {
            float damage = (Ability.DamageCount + (Ability.DamageCount * P_Stats.Instance.PhysicDamageAmplifier)) * GameManager.Instance.DamageMultiplier;
            bool isCriticalHit = Random.Range(0f, 1f) < P_Stats.Instance.CriticalChance;
            if (isCriticalHit)
            {
                damage *= ( 1 * P_Stats.Instance.CriticalDamageAmplifier);
            }
            target.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log(damage);
        }
    }
    public float RadiusSystem(AbilityBase Ability)
    {
        float radius = (Ability.Radius + (Ability.Radius * P_Stats.Instance.RadiusAmplifier));
        return radius;
    }

    public void PushForceSystem(Collider target, AbilityBase Ability, float PushForce, float RecoveryTime, Vector3 position)
    {
        float pushForce = Ability.PushForce + (Ability.PushForce * P_Stats.Instance.PushForceAmplifier);
        target.gameObject.GetComponent<Enemy>().ApplyImpulseAndRecover(PushForce, RecoveryTime, position);
    }
    // PUSHFORCESYSTEM
}
