using System.Collections.Generic;
using UnityEngine;

public class AbilityPickerMenu : MonoBehaviour
{
    [SerializeField] private Transform abilityContainer;
    private List<Ability> abilities = new List<Ability>();
    [SerializeField] private AbilityUI[] abilitySlots;


    public void GetOBJ(List<Ability> m_abilities)
    {
        abilities = m_abilities;

        DisplayAbilities();
        this.gameObject.SetActive(false);
    }

    private void DisplayAbilities()
    {
        for (int i = 0; i < abilitySlots.Length && i < abilities.Count; i++)
        {
            abilitySlots[i].SetAbility(abilities[i]);
        }
    }
}

