using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private Image abilityImage;
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private TMP_Text abilityDescription;
    [SerializeField] private Button selectButton;

    private AbilityInventory _abilityInventory;
    private AbilityPickerMenu _abilityPickerMenu;

    private Ability ability;
    private int index = 0;

    private void Start()
    {
        _abilityInventory = GameObject.Find("AbilityInventory").GetComponent<AbilityInventory>();
        _abilityPickerMenu = GameObject.Find("AbilityPickerMenu").GetComponent<AbilityPickerMenu>();
    }
    public void SetAbility(Ability ability)
    {
        if (ability == null)
        {
            Debug.LogWarning("Ability is null!");
            return;
        }

        this.ability = ability;
        abilityName.text = ability.abilityName;
        abilityDescription.text = ability.abilityDescription;

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(SelectAbility);
    }

    private void SelectAbility()
    {
        index = ability.abilityId; 
        _abilityInventory.AddAbility(index);
        _abilityPickerMenu.SetActive(false);
    }
}
