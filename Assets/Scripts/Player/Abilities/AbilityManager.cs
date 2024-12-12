using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance { get; private set; }
    private List<Ability> allAbilities = new List<Ability>();
    [SerializeField] private AbilityPickerMenu _abilityPickerMenu;

    void Awake()
    {
        if (Instance == null)   
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAbilities();
            PostOBJ();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadAbilities()
    {
        allAbilities.Add(new ArcaneBolt()); 
        allAbilities.Add(new OrbitalSpirits());
    }

    /*private Ability CreateAbility(string name, string damageType, float damageCount, string description, float cooldown, float projectileCount, float range, float duration, string code, int id)
    {
        GameObject abilityObject = new GameObject(name);
        Ability ability = abilityObject.AddComponent<Ability>();
        ability.abilityName = name;
        ability.abilityDamageType = damageType;
        ability.abilityDamageCount = damageCount;
        ability.abilityDescription = description;
        ability.abilityCooldown = cooldown;
        ability.abilityProjectileCount = projectileCount;
        ability.abilityRange = range;
        ability.abilityDuration = duration;
        ability.abilityCode = code;
        ability.abilityId = id;

        return ability;
    }*/

    private void PostOBJ()
    {
        if (_abilityPickerMenu != null)
        {
            _abilityPickerMenu.GetOBJ(allAbilities);
        }
    }
}
