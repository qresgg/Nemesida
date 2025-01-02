using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInventory : MonoBehaviour
{
    [SerializeField] private Image[] slots;
    private List<Ability> abilitiesList = new List<Ability>();
    private string _innateAbility;

    Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        _innateAbility = player._innateAbilityCode;

            Sprite sprite = GetSpriteByAbilityCode(_innateAbility); 
            if (sprite != null) 
            { 
                slots[0].sprite = sprite; 
            } 
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
            if(ability.Code == abilityCode)
            {
                Debug.Log(ability.IconPath);
                return Resources.Load<Sprite>(ability.IconPath);
            }
        }
        return null;
    }
}
