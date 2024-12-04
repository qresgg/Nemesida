using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickerMenu : MonoBehaviour
{
    public static AbilityPickerMenu Instance { get; private set; }
    [SerializeField] private Transform abilityContainer;
    private List<Ability> abilities = new List<Ability>();
    [SerializeField] private AbilityUI[] abilitySlots;
    private string _innateAbility;
    private AbilityInventory _abilityInventory;
    private Player _player;
    private GameManager _gameManager;

    private void Start()
    {
        _abilityInventory = GameObject.Find("AbilityInventory").GetComponent<AbilityInventory>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _innateAbility = _player._innateAbilityCode;
        Debug.Log(_innateAbility);

        ShuffleAbilities();
        DisplayAbilities();
        _gameManager.PauseGame();
    }
    private void Awake()
    {
        if (Instance != null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GetOBJ(List<Ability> m_abilities)
    {
        abilities = m_abilities;
    }

    private void ShuffleAbilities()
    {
        System.Random random = new System.Random();
        int n = abilities.Count;

        for (int i = 0; i < n; i++)
        {
            int randomIndex = random.Next(i, n);
            Ability temp = abilities[i];
            abilities[i] = abilities[randomIndex];
            abilities[randomIndex] = temp;
        }
    }

    private void DisplayAbilities()
    {
        int slotIndex = 0;
        for (int i = 0; i < abilities.Count && slotIndex < abilitySlots.Length; i++)
        {
            if (abilities[i].abilityCode != _innateAbility)
            {
                abilitySlots[slotIndex].SetAbility(abilities[i]);
                Debug.Log($"Ability added to slot {slotIndex}: {abilities[i].abilityCode}");
                Debug.Log(_innateAbility);
                slotIndex++;
            }
            else
            {
                Debug.Log($"Skipped innate ability: {abilities[i].abilityCode}");
            }
        }

        Debug.Log("DisplayAbilities method completed.");
    }

    public void SetActive(bool arg)
    {
        this.gameObject.SetActive(arg);
    }
}
