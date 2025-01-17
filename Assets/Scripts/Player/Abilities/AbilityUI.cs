using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private TMP_Text IsNewAbility;
    [SerializeField] private Image abilitySprite;
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private TMP_Text abilityDescription;
    [SerializeField] private Button selectButton;

    private AbilityInventory _abilityInventory;
    private AbilityPickerMenu _abilityPickerMenu;
    private GameManager _gameManager;
    private Player _player;

    private void Start()
    {
        _abilityInventory = GameObject.Find("AbilityInventory").GetComponent<AbilityInventory>();
        _abilityPickerMenu = GameObject.Find("AbilityPickerMenu").GetComponent<AbilityPickerMenu>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log("ABILITYUI STARTED");
    }

    public void SetAbility(Ability ability)
    {
        if (ability == null)
        {
            abilitySprite.sprite = Resources.Load<Sprite>("Images/UI/AbilityIcons/default");

            abilityName.text = "DEF";
            abilityDescription.text = "DEF";
            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(() => CloseAbilityPickerMenu());
        }
        else
        {
            IsNewAbility.text = ability.IsNewAbility ? "new ability!" : $"level 1";
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
        _abilityInventory.AddAbility(ability); // add to UI Inventory
        _player.AddAbilityToActiveList(ability.Code);

        CloseAbilityPickerMenu();

        Debug.Log($"Гравець обрав здатність: {ability.Name}");
        _abilityPickerMenu.AddPickedAbility(ability.Code);
    }

    private void CloseAbilityPickerMenu()
    {
        _abilityPickerMenu.SetActive(false);
        _gameManager.ResumeGame();
    }
    private void DefaultSprite()
    {
        this.abilitySprite.sprite = Resources.Load<Sprite>("Images/UI/AbilityIcons/default");
    }
}
