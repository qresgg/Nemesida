using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private Image abilitySprite;
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private TMP_Text abilityDescription;
    [SerializeField] private Button selectButton;

    private AbilityInventory _abilityInventory;
    private AbilityPickerMenu _abilityPickerMenu;
    private GameManager _gameManager;

    private List<Ability> _abilitiesDisplayed = new List<Ability>();
    Ability m_ability;

    private void Start()
    {
        _abilityInventory = GameObject.Find("AbilityInventory").GetComponent<AbilityInventory>();
        _abilityPickerMenu = GameObject.Find("AbilityPickerMenu").GetComponent<AbilityPickerMenu>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log("ABILITYUI STARTED");
    }

    public void SetAbility(Ability ability)
    {
        if (ability == null)
        {
            abilitySprite.sprite = Resources.Load<Sprite>("Images/UI/AbilityIcons/OrbitalSpheres/default");

            abilityName.text = "DEF";
            abilityDescription.text = "DEF";
            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(() => CloseAbilityPickerMenu());
        }

        else
        {
            abilitySprite.sprite = Resources.Load<Sprite>(ability.IconPath);

            abilityName.text = ability.Name;
            abilityDescription.text = ability.Description;

            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(() => SelectAbility(ability));

            Debug.Log(ability.Code);
            //_abilitiesDisplayed.Add(ability);
        }
    }

    private void SelectAbility(Ability ability)
    {
        _abilityInventory.AddAbility(ability);
        CloseAbilityPickerMenu();

        Debug.Log($"Гравець обрав здатність: {ability.Name}");
        _abilityPickerMenu.AddPickedAbility(ability.Code);
    }

    private void CloseAbilityPickerMenu()
    {
        _abilityPickerMenu.SetActive(false);
        _gameManager.ResumeGame();
    }
}
