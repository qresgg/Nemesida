using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance { get; private set; }
    private List<Ability> allAbilities = new List<Ability>();
    private List<Ability> abilitiesFiltered = new List<Ability>();
    [SerializeField] private AbilityPickerMenu _abilityPickerMenu;

    private string _innateAbility;

    private AbilityInventory _abilityInventory;
    private Player _player;

    void Awake()
    {
        if (Instance == null)   
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAbilities();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _abilityInventory = GameObject.Find("AbilityInventory").GetComponent<AbilityInventory>();
        _player = GameObject.Find("Player").GetComponent<Player>();

        _innateAbility = GameManager.Instance.InnateAbilityCode;
        UpdateAbilityByCode(_innateAbility);
    }
    private void LoadAbilities()
    {
        var abilityTypes = Assembly.GetExecutingAssembly().GetTypes()
        .Where(t => t.GetCustomAttributes(typeof(AbilityAttribute), false).Length > 0);

        foreach (var type in abilityTypes)
        {
            var abilityInstance = Activator.CreateInstance(type) as Ability;
            if (abilityInstance != null)
            {
                allAbilities.Add(abilityInstance);
            }
        }
    }
    public List<Ability> GetAbilityList()
    {
        return allAbilities;
    }
    private void UpdateAbilityByCode(string code)
    {
        Ability ability = FindAbilityByCode(code);
        if (ability != null)
        {
            ability.UpgradeAbility();
        }
        else
        {
            Debug.LogWarning($"Ability with code {code} not found.");
        }
    }
    private Ability FindAbilityByCode(string code)
    {
        foreach (Ability ability in allAbilities)
        {
            if (ability.Code == code)
            {
                return ability;
            }
        }
        return null;
    }
}
