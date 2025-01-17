using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance { get; private set; }
    private List<Ability> allAbilities = new List<Ability>();
    [SerializeField] private AbilityPickerMenu _abilityPickerMenu;

    private string _innateAbility;

    private AbilityInventory _abilityInventory;
    private Player _player;
    private GameManager _gameManager;

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
    private void Start()
    {
        _abilityInventory = GameObject.Find("AbilityInventory").GetComponent<AbilityInventory>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _innateAbility = _gameManager.GetInnateAbilityCode();
        _abilityInventory.GetOBJ(allAbilities);
    }
    private void LoadAbilities()
    {
        allAbilities.Add(new Fireball()); 
        allAbilities.Add(new OrbitalSpheres());
        allAbilities.Add(new Whirligig());
    }

    private void PostOBJ()
    {
        List<Ability> abilities = allAbilities;
        abilities.RemoveAll(ability => ability.Code == _innateAbility);
        if (_abilityPickerMenu != null)
        {
            _abilityPickerMenu.GetOBJ(abilities);
        }
    }
}
