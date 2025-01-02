using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance { get; private set; }
    private List<Ability> allAbilities = new List<Ability>();
    [SerializeField] private AbilityPickerMenu _abilityPickerMenu;

    private AbilityInventory _abilityInventory;

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
        if (_abilityPickerMenu != null)
        {
            _abilityPickerMenu.GetOBJ(allAbilities);
        }
    }
}
