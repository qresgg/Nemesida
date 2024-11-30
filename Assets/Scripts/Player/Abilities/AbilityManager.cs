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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadAbilities();
        PostOBJ();
    }

    private void LoadAbilities()
    {
        allAbilities = new List<Ability>
        {
            CreateAbility("Arcane Bolt", "Magical", 7.5f, "Shoots a damaging projectile at the closest enemy.", 1.2f, 2, 1200, 10, "arcane_bolt", 3),
            CreateAbility("Stone", "Magical", 5f, "21312", 2f, 5, 1200, 10, "stone", 2),
            CreateAbility("Chain Frost", "Magical", 60f, "Shoots a slow-moving projectile at the closest enemy, dealing damage.", 14f, 1, 800, 10, "chain_frost", 1)
        };
    }

    private Ability CreateAbility(string name, string damageType, float damageCount, string description, float cooldown, float projectileCount, float range, float duration, string code, int id)
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
    }

    private void PostOBJ()
    {
        if (_abilityPickerMenu != null)
        {
            _abilityPickerMenu.GetOBJ(allAbilities);
        }
    }
}
