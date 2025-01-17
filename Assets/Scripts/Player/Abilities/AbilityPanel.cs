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

    Player _player;
    GameManager _gameManager;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        InnateAbilityBookSlot();
    }
    public void GetOBJ(List<Ability> abilities)
    {
        abilitiesList = abilities;
    }

    public void AddAbility(Ability ability)
    {
        for (int i = 1; i < slots.Length; i++)
        {
            if (slots[i].sprite == null)
            {
                Sprite sprite = Resources.Load<Sprite>(ability.IconPath);
                if (sprite != null)
                {
                    slots[i].sprite = sprite;
                }
                return;
            }
        }
        Debug.Log("No empty slots available");
    }

    private Sprite GetSpriteByAbilityCode(string abilityCode)
    {
        foreach (Ability ability in abilitiesList)
        {
            if (ability.Code == abilityCode)
            {
                Debug.Log(ability.IconPath);
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
        }
    }
}
