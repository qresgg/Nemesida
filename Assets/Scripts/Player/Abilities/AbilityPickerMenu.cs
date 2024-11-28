using System.Collections.Generic;
using UnityEngine;

public class AbilityPickerMenu : MonoBehaviour
{
    [SerializeField] private Transform abilityContainer;
    private List<Ability> abilities = new List<Ability>();
    [SerializeField] private AbilityUI[] abilitySlots;
    private string _innerAbility;

    public void GetOBJ(List<Ability> m_abilities)
    {
        abilities = m_abilities;

        ShuffleAbilities();
        DisplayAbilities();
        //this.gameObject.SetActive(false);
    }

    public void GetInnateAbility(string get_innerAbility)
    {
        _innerAbility = get_innerAbility;
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
            if (abilities[i].abilityCode != _innerAbility)
            {
                abilitySlots[slotIndex].SetAbility(abilities[i]);
                slotIndex++;
            }
        }
    }
}
