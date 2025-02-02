using System;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInventory : MonoBehaviour
{
    [SerializeField] private Image[] slots;
    private List<Ability> abilitiesList = new List<Ability>();
    private string _innateAbility;

    List<Ability> activeAbilities = new List<Ability>();

    Dictionary<string, int> abilitiesDictionary = new Dictionary<string, int>();

    Player _player;
    GameManager _gameManager;
    AbilityManager _abilityManager;
    AbilityUICooldowns _abilityUICooldowns;
    P_AbilityUser player_abilityUser;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _abilityManager = GameObject.Find("AbilityManager").GetComponent<AbilityManager>();
        _abilityUICooldowns = GameObject.Find("Cooldowns").GetComponent<AbilityUICooldowns>();
        player_abilityUser = GameObject.Find("P_AbilityUser").GetComponent<P_AbilityUser>();

        abilitiesList = _abilityManager.GetAbilityList();
        InnateAbilityBookSlot();
    }
    private void Update()
    {
        AbilitySlotsInitializer();
    }
    public void AddAbility(Ability ability)
    {
        if (!abilitiesDictionary.ContainsKey(ability.Code))
        {
            for (int i = 1; i < slots.Length; i++)
            {
                if (slots[i].sprite == null)
                {
                    Sprite sprite = Resources.Load<Sprite>(ability.IconPath);
                    if (sprite != null)
                    {
                        Color newColor = slots[i].color;
                        newColor.a = 0;
                        slots[i].color = newColor;

                        slots[i].sprite = sprite;
                        abilitiesDictionary.Add(ability.Code, i);
                        player_abilityUser.SetAbilityDictionary(abilitiesDictionary);
                    }
                    return;
                }
            }
            Debug.Log("No empty slots available");
        } else
        {
            Debug.Log("CONTAINS");
        }
    }

    private Sprite GetSpriteByAbilityCode(string abilityCode)
    {
        foreach (Ability ability in abilitiesList)
        {
            if (ability.Code == abilityCode)
            {
                //Debug.Log(ability.IconPath);
                return Resources.Load<Sprite>(ability.IconPath);
            }
        }
        return null;
    }
    private void InnateAbilityBookSlot()
    {
        _innateAbility = _gameManager.GetInnateAbilityCode();
        Sprite sprite = GetSpriteByAbilityCode(_innateAbility);
        if (sprite != null)
        {
            slots[0].sprite = sprite;
            abilitiesDictionary.Add(_innateAbility, 0);
        }
    }
    private void AbilitySlotsInitializer()
    {
        foreach (Image img in slots)
        {
            Color newColor = img.color;
            if (img.sprite == null)
            {
                newColor.a = 0;
                img.color = newColor;
            }
            else
            {
                newColor.a = 1;
                img.color = newColor;
            }
        }
    }
}
