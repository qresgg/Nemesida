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
        allAbilities.Add(new Copy());
    }

    private void PostOBJ()
    {
        if (_abilityPickerMenu != null)
        {
            _abilityPickerMenu.GetOBJ(allAbilities);
        }
    }
}
