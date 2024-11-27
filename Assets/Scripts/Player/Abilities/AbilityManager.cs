using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance { get; private set; }
    private List<Ability> allAbilities = new List<Ability>();
    private AbilityPickerMenu _abilityPickerMenu;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadAbilities();
        _abilityPickerMenu = FindObjectOfType<AbilityPickerMenu>();
        PostOBJ();
    }

    private void LoadAbilities()
    {
        allAbilities = new List<Ability>
        {
            CreateAbility("Arcane Bolt", "Magical", 7.5f, "Shoots a damaging projectile at the closest enemy.", 1.2f, 2, 1200, 10, "arcane_bolt"),
            CreateAbility("Stone", "Magical2", 5f, "21312", 2f, 5, 1200, 10, "stone")
        };
    }

    private Ability CreateAbility(string name, string damageType, float damageCount, string description, float cooldown, float projectileCount, float range, float duration, string code)
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

        return ability;
    }

    private void PostOBJ()
    {
        _abilityPickerMenu.GetOBJ(allAbilities);
    }
}
